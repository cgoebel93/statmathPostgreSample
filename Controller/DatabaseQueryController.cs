using statmathPostgreSample.Database;
using statmathPostgreSample.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace statmathPostgreSample.Controller
{
    class DatabaseQueryController
    {
        public DatabaseQueryController()
        {

        }

        #region GenericQueries

        /// <summary>
        /// Get all entries from one entity type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllEntries<T>() where T : Entity
        {
            List<T> result = null;
            using (MachineModel model = new MachineModel())
            {
                result = model.Set<T>().ToList();
            }
            return result;
        }

        /// <summary>
        /// Delete all entries from one entity type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DeleteAllEntries<T>() where T : Entity
        {
            using (MachineModel model = new MachineModel())
            {
                model.Set<T>().RemoveRange(model.Set<T>().ToList());
                model.SaveChanges();
            }
        }

        /// <summary>
        /// Check if the related entity table contains entries
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool CheckIfTableEmpty<T>() where T : Entity
        {
            using (MachineModel model = new MachineModel())
            {
                if (model.Set<T>().FirstOrDefault() != null)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Insert a list of entites into the related database table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertObjects"></param>
        public void InsertObjectsIntoDatabase<T>(List<T> insertObjects) where T : Entity
        {
            using (MachineModel model = new MachineModel())
            {
                model.Set<T>().AddRange(insertObjects);
                model.SaveChanges();
            }
        }
        #endregion

        #region SpecificQueries

        /// <summary>
        /// Returns all machineJob entities and their related machines
        /// </summary>
        /// <returns></returns>
        public List<MachineJob> GetAllMachineJobs()
        {
            List<MachineJob> machineJobs = null;

            using (MachineModel model = new MachineModel())
            {
                machineJobs = model.machinejobs.Include(x => x.Machine).ToList();

                foreach (MachineJob job in machineJobs)
                    model.Entry(job).Reference(x => x.Machine).Load();
            }

            return machineJobs;
        }
        #endregion

    }
}
