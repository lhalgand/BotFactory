using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using BotFactory.Models;

namespace BotFactory.Factories
{
    public class UnitFactory
    {
        public int QueueSize;                           // Taille de la queue
        public int StorageSize;                         // Taille de l’entrepôt

        public int QueueCapacity;                       // Nombre de robots dans la queue
        public int StorageCapacity;                     // Nombre de robots construits

        public List<FactoryQueueElement> Queue;
        //public List<ITestingUnit> Storage;
        public List<IWorkingUnit> Storage;
        
        private BackgroundWorker backgroundWorker;      // Thread de construction des robots

        public UnitFactory(int queueSize, int storageSize)
        {
            this.QueueSize = queueSize;
            this.StorageSize = storageSize;

            this.Queue = new List<FactoryQueueElement>();
            //this.Storage = new List<ITestingUnit>();
            this.Storage = new List<IWorkingUnit>();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = false;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
        }

        /// <summary>
        /// Evènement d'ajout de commande de robot à la queue selon les conditions suivantes :
        ///     - [ ] L’usine ne peut construire qu’un robot à la fois
        ///     - [ ] L’usine ne peut enregistrer plus de commandes si sa queue est pleine
        ///     - [ ] L’usine ne peut construire plus de robots si son entrepôt est plein
        ///     - [ ] On peut appeler l’ajout de commande n’importe quand
        ///     - [ ] La méthode doit retourner false si la commande n’est pas enregistrée
        ///     - [ ] La construction doit être active tant que la queue n’est pas vide ou l’entrepôt plein
        ///     - [ ] La construction d’un robot doit être simulée et prendre le temps indiqué par la propriété BuildTime du robot
        /// </summary>
        /// <returns>Retourne un indicateur permettant de savoir si la commande à été ajoutée</returns>
        public bool AddWorkableUnitToQueue(string name, Type model, Coordinates parkingPos, Coordinates workingPos)
        {
            if (this.QueueCapacity < this.QueueSize &&
                this.StorageCapacity < this.StorageSize)
            {
                // Création d'un nouveau robot à créer dans la queue
                FactoryQueueElement queueElement = new FactoryQueueElement(name, model, parkingPos, workingPos);
                this.Queue.Add(queueElement);
                this.QueueCapacity -= 1;

                // Usine pas occupée ==> lancement de la phase de fabrication de robot
                if (backgroundWorker.IsBusy != true)
                { this.backgroundWorker.RunWorkerAsync(this.Queue.First()); }
            }

            return false;
        }

        /// <summary>
        /// Evènement de construction d'un robot dans l'usine
        /// </summary>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            object obj = e.Argument;
            if (obj is FactoryQueueElement)
            {
                FactoryQueueElement queueElement = obj as FactoryQueueElement;
                if (queueElement != null)
                {
                    // Suppresssion du robot de la queue
                    this.Queue.Remove(queueElement);
                    this.QueueCapacity += 1;

                    IWorkingUnit workingUnit = (IWorkingUnit)Activator.CreateInstance(queueElement.Model);
                    workingUnit.ParkingPos = queueElement.ParkingPos;
                    workingUnit.WorkingPos = queueElement.WorkingPos;
                    workingUnit.WorkBegins();
                    e.Result = workingUnit;
                }
            }
        }

        /// <summary>
        /// Evènement de fin de construction d'un robot dans l'usine
        /// </summary>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IWorkingUnit workingUnit = e.Result as IWorkingUnit;
            if (workingUnit != null)
            {
                workingUnit.WorkEnds();
                this.Storage.Add(workingUnit);

                // Usine pas occupée ==> lancement de la phase de fabrication de robot
                if (this.QueueCapacity > 0)
                { this.backgroundWorker.RunWorkerAsync(this.Queue.First()); }
            }
        }
    }
}
