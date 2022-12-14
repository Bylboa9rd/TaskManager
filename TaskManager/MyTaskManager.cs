using System.Diagnostics;

namespace TaskManager
{
    internal static class MyTaskManager
    {
        //task manager that
        //displays a list of running processes
        //starts/kills processes by name/id
        //create custom processes and kills them
        //creates custom threads (plus background threads) and kills them
        //starts an existing process in the system
        //checks if a thread is alive and is background


        public static async Task DisplayRunningProcessesAsync()
        {
            await Task.Run(() =>
            {
                var runningProcesses = from process in Process.GetProcesses()
                                       orderby process.Id
                                       select process;
                foreach (var process in runningProcesses)
                {
                    Console.WriteLine("-----------------\nRunning processes:\n-----------------");
                    string info = $"Process ID: {process.Id} \t\t Process Name: {process.ProcessName}";
                    Console.WriteLine(value: info);
                }
            });
        }

        public static async Task StartExistingProcessAsync()
        {
            await Task.Run(() =>
            {
                DisplayRunningProcessesAsync();
                Console.WriteLine("Enter the ID of the process you would like to start: ");

                try
                {
                    int processId = int.Parse(Console.ReadLine());

                    Process _ = Process.GetProcessById(processId);
                    _.Start();

                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"An error occured. {ex.Message}");
                }


            });
        }

        public static async Task KillProcessAsync()
        {
            await Task.Run(() =>
            {
                /*static void ByName()
                {
                    DisplayRunningProcessesAsync();
                    Console.WriteLine("Enter the name of the process you would like to stop: ");

                    try
                    {
                        string processName = Console.ReadLine();

                        Process _ = Process.GetProcessesByName(processName);
                        _.Kill();

                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"An error occured. {ex.Message}");
                    }
                }*/

                DisplayRunningProcessesAsync();
                Console.WriteLine("Enter the ID of the process you would like to stop: ");

                try
                {
                    int processId = int.Parse(Console.ReadLine());

                    Process _ = Process.GetProcessById(processId);
                    _.Kill();

                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"An error occured. {ex.Message}");
                }

            });
        }

        public static async Task CreateCustomProcessAsync()
        {
            await Task.Run(() =>
            {
                Process process = null;

                try
                {
                    process = Process.Start(@"", "www.google.com");
                    Console.WriteLine($"Name of created process  : {process?.ProcessName}");

                    Console.WriteLine($"Press enter to kill the current process {process.ProcessName}");
                    Console.ReadLine();

                    try
                    {
                        foreach (var p in Process.GetProcessesByName(process?.ProcessName))
                        {
                            p.Kill(true);
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"An error occured. {ex.Message}");
                    }



                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"An error occured. {ex.Message}");
                }


            });
        }

        public static async Task CreateCustomThreadAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("How many threads would you like to create [1] or [2]? ");

                try
                {
                    void KeepChugging()
                    {
                        Console.WriteLine($"{Thread.CurrentThread.Name} is currently chugging palm wine.");
                        Console.WriteLine("Chug! Chug! Chug! Chug! Chug!");
                    }

                    void Secondary()
                    {
                        ThreadStart startThread = new ThreadStart(KeepChugging);
                        Thread thread = new Thread(startThread);
                        thread.Name = "My Custom Thread";
                        thread.IsBackground = true;
                        thread.Start();
                    }

                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            KeepChugging();
                            break;
                        case 2:
                            Secondary();
                            break;
                        default:
                            Console.WriteLine("Please enter either 1 or 2");
                            break;
                    }

                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"An error occured. {ex.Message}");
                }
            });
        }

        public static async Task CheckThreadPropertiesAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Would you like to check the thread's properties? [1]Alive or not? [2]Is background or not [3] Not interested");

                try
                {
                    int response = int.Parse(Console.ReadLine());

                    switch (response)
                    {
                        case 1:
                            bool isAlive = Thread.CurrentThread.IsAlive;
                            Console.WriteLine(isAlive ? "Thread is alive and running" : "Whoops, thread is not alive");
                            break;
                        case 2:
                            bool isBackground = Thread.CurrentThread.IsBackground;
                            if (isBackground == false)
                            {
                                Console.WriteLine("Nope, current thread is not a background thread.");
                            }
                            Console.WriteLine("Yup, current thread is a bacground thread");
                            break;
                        case 3:
                            break;
                        default:
                            Console.WriteLine("Invalid option! Please choose from options 1 - 3");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"An error occured. {ex.Message}");
                }
                
            });
        }

        /*
                    }*/

    }
}
