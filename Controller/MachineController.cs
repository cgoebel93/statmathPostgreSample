using statmathPostgreSample.Database;
using statmathPostgreSample.Database.Entities;
using statmathPostgreSample.Enums;
using statmathPostgreSample.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace statmathPostgreSample.Controller
{
    public class MachineController
    {
        private DatabaseQueryController queryController = new DatabaseQueryController();
        public MachineController()
        {

        }

        /// <summary>
        /// Print all valid user options to console
        /// </summary>
        public void ShowOptions()
        {
            Console.WriteLine(Environment.NewLine + Resource1.MachineControllerOptions + Environment.NewLine);
            string unserInput = Console.ReadLine();

            if (int.TryParse(unserInput, out int result))
            {
                Console.WriteLine();

                switch ((MachineControllerOptions)result)
                {
                    case MachineControllerOptions.ListAllMachines:
                        {
                            ListAllMachines();
                            break;
                        }
                    case MachineControllerOptions.ListAllJobs:
                        {
                            ListAllMachineJobs();
                            break;
                        }
                    case MachineControllerOptions.DeleteEverything:
                        {
                            DeleteAll();
                            break;
                        }
                    case MachineControllerOptions.InsertFromCSV:
                        {
                            InsertFromCSV();
                            break;
                        }
                    case MachineControllerOptions.EndProgramm:
                        {
                            EndProgramm();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(Resource1.MachineControllerWrongInput);
                            ShowOptions();
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine(Resource1.MachineControllerWrongInput);
                ShowOptions();
            }
        }

        /// <summary>
        /// Print all machines to console
        /// </summary>
        public void ListAllMachines()
        {
            foreach (Machine item in queryController.GetAllEntries<Machine>())
                Console.WriteLine($"{Resource1.Machine}: {item.name}");

            ShowOptions();
        }

        /// <summary>
        /// Delete all machines and jobs from database
        /// </summary>
        public void DeleteAll()
        {
            queryController.DeleteAllEntries<MachineJob>();
            queryController.DeleteAllEntries<Machine>();
            Console.WriteLine($"{Resource1.MachineControllerDeleteDone}");

            ShowOptions();
        }

        /// <summary>
        /// Print all machines and jobs in console
        /// </summary>
        public void ListAllMachineJobs()
        {
            foreach (MachineJob job in queryController.GetAllMachineJobs())
                Console.WriteLine($"*{Resource1.Job}-{job.id} {Resource1.Machine}-{job.Machine.name.Trim()}: {job.startdate} - {job.enddate}");

            ShowOptions();
        }

        public void EndProgramm()
        {
            System.Environment.Exit(0);
        }

        /// <summary>
        /// Insert machines from a csv into database if table is empty
        /// </summary>
        public void InsertFromCSV()
        {
            try
            {
                //Prevent multiple file import
                if (queryController.CheckIfTableEmpty<MachineJob>() == false)
                {
                    Console.WriteLine(Resource1.FileWasAlreadyInserted);
                    ShowOptions();
                    return;
                }
                

                List<MachineJob> machineJobs = new List<MachineJob>();
                List<Machine> machines = new List<Machine>();

                string plan = Resource1.plan;
                string[] splittedPlan = plan.Split(new string[] { Environment.NewLine, "\r\n", "\r", "\n" }, StringSplitOptions.None);
                int machineID = 1;

                foreach (string line in splittedPlan)
                {
                    var values = line.Split(';');

                    if (String.IsNullOrEmpty(line) ||  values[0].Equals("machine"))
                        continue;

                    MachineJob newMachineJob = new MachineJob();

                    Machine newMachine = machines.Where(x => x.name.Equals(values[0])).FirstOrDefault();
                    if (newMachine == null)
                    {
                        newMachine = new Machine();
                        newMachine.id = machineID++;
                        newMachine.name = values[0];
                        machines.Add(newMachine);
                    }

                    newMachineJob.Machine = newMachine;

                    if (int.TryParse(values[1], out int result))
                        newMachineJob.id = result;

                    if (DateTime.TryParseExact(values[2], "yyyy-MM-dd-HH-mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime startDate))
                    {
                        newMachineJob.startdate = startDate;
                    }
                    if (DateTime.TryParseExact(values[3], "yyyy-MM-dd-HH-mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime endDate))
                    {
                        newMachineJob.enddate = endDate;
                    }

                    machineJobs.Add(newMachineJob);
                }

                //Save jobs and machines to DB
                queryController.InsertObjectsIntoDatabase(machineJobs);

                Console.WriteLine($"{Resource1.MachineControllerInsertDone}");

                ShowOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Resource1.MachineControllerCsvImportFailed}:{Environment.NewLine + ex.ToString()}");
            }
        }
    }
}
