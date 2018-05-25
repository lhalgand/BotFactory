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
    /// <summary>
    /// Usine de construction de robots :
    ///     - l’usine ne peut construire qu’un robot à la fois
    ///     - l’usine ne peut enregistrer plus de commandes si sa queue est pleine
    ///     - l’usine ne peut construire plus de robots si son entrepôt est plein
    ///     - on peut appeler l’ajout de commande n’importe quand
    ///     - la méthode doit retourner false si la commande n’est pas enregistrée
    ///     - la construction doit être active tant que la queue n’est pas vide ou l’entrepôt plein
    ///     - la construction d’un robot doit être simulée et prendre le temps indiqué par la propriété BuildTime du robot
    /// </summary>
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

        public event EventHandler FactoryProgress;

        public UnitFactory(int queueCapacity, int storageCapacity)
        {
            this.QueueCapacity = queueCapacity;
            this.QueueFreeSlots = queueCapacity;

            this.StorageCapacity = storageCapacity;
            this.StorageFreeSlots = storageCapacity;

            this.Queue = new List<IFactoryQueueElement>();
            this.Storage = new List<ITestingUnit>();
        }

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
                
                //Parallel.Invoke(() => this.BuildRobot(queueElement));
                Task task = Task.Run(() => this.BuildRobot(queueElement));
                task.Wait();
                return true;
            }

            return false;
        }

        private void BuildRobot(FactoryQueueElement queueElement)
        {
            if (queueElement == null)
            { return; }

            this.Queue.Remove(queueElement);
            this.QueueFreeSlots += 1;

            IWorkingUnit workingUnit = (IWorkingUnit)Activator.CreateInstance(queueElement.Model.UnderlyingSystemType, queueElement.Name, 0);
            workingUnit.Model = queueElement.Model;
            workingUnit.ParkingPos = queueElement.ParkingPos;
            workingUnit.WorkingPos = queueElement.WorkingPos;
            workingUnit.WorkBegins();
            workingUnit.WorkEnds();

            //this.Storage.Add(workingUnit);

            //// Usine signale que la construction d'un robot s'achève
            //if (this.FactoryProgress != null)
            //{ this.FactoryProgress(this, new EventArgs()); }

            TimeSpan ts = TimeSpan.FromMilliseconds(workingUnit.BuildTime);
            this.QueueTime = this.QueueTime.Add(ts);
        }
    }
}
