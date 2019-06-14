
namespace StartDotNet
{


    public class Program
    {

        private static string GetLatestVisualStudio()
        {
            string retValue = null;
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\VisualStudio\\SxS\\VS7");

            // Assuming they are in ascending order 
            foreach (string valueName in key.GetValueNames())
            {
                object objKeyValue = key.GetValue(valueName);
                
                retValue = System.Convert.ToString(objKeyValue, System.Globalization.CultureInfo.InvariantCulture);
            } // Next valueName 

            return retValue;
        } // End Function GetLatestVisualStudio 


        [System.STAThread()] 
        public static void Main(string[] args)
        {
            string dotNetLocationx_32bits = @"D:\Programme\LessPortableApps\dotnet\";
            string dotNetLocationx_64bits = @"D:\Programme\LessPortableApps\dotnet\";
            // string dotNetLocationx_64bits = @"D:\Programme_x64\LessPortableApps\dotnet\";


            string basePath = GetLatestVisualStudio();
            
            // string workingDirectory = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE";
            string workingDirectory = System.IO.Path.Combine(basePath, "Common7", "IDE");
            // string devenv = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe";
            string devenv = System.IO.Path.Combine(basePath, "Common7", "IDE", "devenv.exe");
            string arguments = "";


            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(devenv, arguments);
            psi.UseShellExecute = false;
            psi.WorkingDirectory = workingDirectory;

            // Pass current environment variables - alter dotnet installation location 
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
                                paths[i] = dotNetLocationx_32bits;
                            }
                            else
                            {
                                paths[i] = dotNetLocationx_64bits;
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
