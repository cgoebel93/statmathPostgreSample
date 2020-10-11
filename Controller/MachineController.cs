using statmathPostgreSample.Database;
using statmathPostgreSample.Database.Entities;
using statmathPostgreSample.Enums;
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
        public MachineController()
        {

        }

        /// <summary>
        /// Print all valid user options to console
        /// </summary>
        public void ShowOptions()
        {
            Console.WriteLine(Environment.NewLine + Properties.Resource1.MachineControllerOptions + Environment.NewLine);
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
                            DeleteAllMachines();
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
                            Console.WriteLine(Properties.Resource1.MachineControllerWrongInput);
                            ShowOptions();
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine(Properties.Resource1.MachineControllerWrongInput);
                ShowOptions();
            }
        }

        /// <summary>
        /// Print all Machines in console
        /// </summary>
        public void ListAllMachines()
        {
            using (MachineModel model = new MachineModel())
            {
                var machines = model.Machines.ToList();

                foreach (var item in machines)
                    Console.WriteLine(item.Name);
            }

            ShowOptions();
        }

        public void ReadMachinesFromCSV()
        {
         
            ShowOptions();
        }

        /// <summary>
        /// Delete all machines and jobs from database
        /// </summary>
        public void DeleteAllMachines()
        {
            using (MachineModel model = new MachineModel())
            {
                model.Machines.RemoveRange(model.Machines.ToList());
                model.SaveChanges();
            }

            ShowOptions();

        }

        /// <summary>
        /// Print all machines and jobs in console
        /// </summary>
        public void ListAllMachineJobs()
        {
            using (MachineModel model = new MachineModel())
            {
                List<MachineJob> machineJobs = model.MachineJobs.Include(x => x.Machine).ToList();

                foreach (MachineJob job in machineJobs)
                {
                    Console.WriteLine($"* {job.Machine.Name}: {job.StartDate} - {job.EndDate}");
                }
            }

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
                using (MachineModel model = new MachineModel())
                {
                    if(model.MachineJobs.FirstOrDefault() != null)
                    {
                        Console.WriteLine("File was already inserted");
                        ShowOptions();
                        return;
                    }
                }


                List<MachineJob> machineJobs = new List<MachineJob>();
                List<Machine> machines = new List<Machine>();

                string csvPath = AppDomain.CurrentDomain.BaseDirectory + @"Database\Scripts\plan.csv";
                using (var reader = new StreamReader(csvPath))
                {

                    int machineID = 1;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        if (String.IsNullOrEmpty(values[0]) || values[0].Equals("machine"))
                        {
                            continue;
                        }

                        MachineJob newMachineJob = new MachineJob();

                        Machine newMachine = machines.Where(x => x.Name.Equals(values[0])).FirstOrDefault();
                        if (newMachine == null)
                        {
                            newMachine = new Machine();
                            newMachine.ID = machineID++;
                            newMachine.Name = values[0];
                            machines.Add(newMachine);
                        }

                        newMachineJob.Machine = newMachine;

                        if(int.TryParse(values[1], out int result))
                            newMachineJob.ID = result;

                        if (DateTime.TryParseExact(values[2], "yyyy-MM-dd-HH-mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime startDate))
                        {
                            newMachineJob.StartDate = startDate;
                        }
                        if (DateTime.TryParseExact(values[3], "yyyy-MM-dd-HH-mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime endDate))
                        {
                            newMachineJob.EndDate = endDate;
                        }

                        machineJobs.Add(newMachineJob);
                    }
                }

                //Save jobs and machines to DB
                using (MachineModel model = new MachineModel())
                {
                    model.MachineJobs.AddRange(machineJobs);
                    var changes2  = model.ChangeTracker.Entries().ToList();
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File plan.csv could not be imported into database: {ex.ToString()}");
            }
            
        }
    }
}
