
namespace StartDotNet
{


    public class Program
    {


        [System.STAThread()] 
        public static void Main(string[] args)
        {
            string devenv = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe";
            string arguments = "";

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(devenv, arguments);
            psi.UseShellExecute = false;
            psi.WorkingDirectory = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE";

            System.Collections.IDictionary p = System.Environment.GetEnvironmentVariables();

            foreach (object k in p.Keys)
            {
                string key = (string)k;
                string value = (string) p[k];

                if (string.Equals(key, "PATH", System.StringComparison.OrdinalIgnoreCase))
                {
                    string[] paths = value.Split(';');
                    for (int i = 0; i < paths.Length; ++i)
                    {

                        if (paths[i].EndsWith(@"dotnet\", System.StringComparison.OrdinalIgnoreCase))
                        {
                            if (paths[i].IndexOf("(x86)", System.StringComparison.OrdinalIgnoreCase) != -1)
                            {
                                // paths[i] = @"D:\Programme_x86\LessPortableApps\dotnet\";
                                paths[i] = @"D:\Programme\LessPortableApps\dotnet\";
                            }
                            else
                            {
                                paths[i] = @"D:\Programme\LessPortableApps\dotnet\";
                            }

                        } // End if (paths[i].EndsWith(@"dotnet\", System.StringComparison.OrdinalIgnoreCase)) 

                        paths[i] = paths[i];
                    } // Next i 
                    psi.EnvironmentVariables[key] = string.Join(";", paths);
                } // End if (string.Equals(key, "PATH", System.StringComparison.OrdinalIgnoreCase))
                else
                    psi.EnvironmentVariables[key] = value;
            } // Next k 


            // https://github.com/dotnet/core/issues/1979
            // https://github.com/dotnet/core/tree/master/release-notes
            // https://github.com/dotnet/core

            // https://stackoverflow.com/questions/53641740/use-visual-studio-2017-with-net-core-sdk-3-0
            // Tools → Options → Project and Solutions → .NET Core 
            // Extras → Optionen → Projekte und Projektmappen → .NET Core → Vorschauversionen des .NET Core SDK verwenden
            System.Diagnostics.Process.Start(psi);
        } // End Sub Main 


    } // End Class Program 


} // End Namespace StartDotNet 
