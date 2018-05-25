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
    public class UnitFactory : IUnitFactory
    {
        // Taille de la queue
        public int QueueCapacity { get; private set; }

        // Taille de l’entrepôt
        public int StorageCapacity { get; private set; }

        // Emplacements disponibles dans la queue
        public int QueueFreeSlots { get; private set; }

        // Emplacements disponibles dans l'entrepôt
        public int StorageFreeSlots { get; private set; }

        public TimeSpan QueueTime { get; private set; }

        public List<IFactoryQueueElement> Queue { get; private set; }

        public List<ITestingUnit> Storage { get; private set; }

        // Thread de construction des robots
        private BackgroundWorker backgroundWorker;

        public event EventHandler FactoryProgress;

        public UnitFactory(int queueCapacity, int storageCapacity)
        {
            this.QueueCapacity = queueCapacity;
            this.QueueFreeSlots = queueCapacity;

            this.StorageCapacity = storageCapacity;
            this.StorageFreeSlots = storageCapacity;

            this.Queue = new List<IFactoryQueueElement>();
            this.Storage = new List<ITestingUnit>();

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
        public bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            if (this.QueueFreeSlots > 0 && this.QueueFreeSlots <= this.QueueCapacity &&
                this.StorageFreeSlots > 0 && this.StorageFreeSlots <= this.StorageCapacity)
            {
                // Création d'un nouveau robot à créer dans la queue
                FactoryQueueElement queueElement = new FactoryQueueElement(name, model, parkingPos, workingPos);
                this.Queue.Add(queueElement);
                this.QueueFreeSlots -= 1;
                this.StorageFreeSlots -= 1;

                // Usine pas occupée ==> lancement de la phase de fabrication de robot
                if (!backgroundWorker.IsBusy)
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
            if (!(obj is IFactoryQueueElement))
            { return; }

            FactoryQueueElement queueElement = obj as FactoryQueueElement;

            // Usine signale que la construction d'un nouveau robot commmence
            if (this.FactoryProgress != null)
            { this.FactoryProgress(this, new EventArgs()); }

            // Suppresssion du robot de la queue
            this.Queue.Remove(queueElement);
            this.QueueFreeSlots += 1;

            IWorkingUnit workingUnit = null;

            if (queueElement.Model == typeof(R2D2))
            { workingUnit = (IWorkingUnit)Activator.CreateInstance(typeof(R2D2), queueElement.Name, 0); }

            if (queueElement.Model == typeof(HAL))
            { workingUnit = (IWorkingUnit)Activator.CreateInstance(typeof(HAL), queueElement.Name, 0); }

            if (queueElement.Model == typeof(T_800))
            { workingUnit = (IWorkingUnit)Activator.CreateInstance(typeof(T_800), queueElement.Name, 0); }

            if (queueElement.Model == typeof(Wall_E))
            { workingUnit = (IWorkingUnit)Activator.CreateInstance(typeof(Wall_E), queueElement.Name, 0); }

            workingUnit.Model = queueElement.Model;
            workingUnit.ParkingPos = queueElement.ParkingPos;
            workingUnit.WorkingPos = queueElement.WorkingPos;
            workingUnit.WorkBegins();
            e.Result = workingUnit;
        }

        /// <summary>
        /// Evènement de fin de construction d'un robot dans l'usine
        /// </summary>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IWorkingUnit workingUnit = e.Result as IWorkingUnit;
            if (workingUnit != null)
            {
                // Usine signale que la construction d'un robot s'achève
                if (this.FactoryProgress != null)
                { this.FactoryProgress(this, new EventArgs()); }

                workingUnit.WorkEnds();
                //this.Storage.Add(workingUnit);

                TimeSpan ts = TimeSpan.FromMilliseconds(workingUnit.BuildTime);
                this.QueueTime = this.QueueTime.Add(ts);

                // Usine pas occupée ==> lancement de la phase de fabrication du robot suivant
                if (this.QueueFreeSlots > 0 && this.QueueFreeSlots < this.QueueCapacity)
                { this.backgroundWorker.RunWorkerAsync(this.Queue.First()); }
            }
        }
    }
}
