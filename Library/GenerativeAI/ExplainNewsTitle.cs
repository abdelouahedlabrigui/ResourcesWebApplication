using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ResourcesWebApplication.Models.News;

namespace ResourcesWebApplication.Library.GenerativeAI
{
    public class ExplainNewsTitle
    {
        public List<NewsTitleDescription> GetNewsTitleDescriptionUsingPowerShell(string title)
        {
            string pythonExe = "python.exe";
            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-NLP\GenerateNewsDescription.py";
            string command = $@"
                $output = & {pythonExe} {scriptPath} ""{title}""
                $output";
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception($"Error occured: {error}");
                }
                List<NewsTitleDescription> result = new List<NewsTitleDescription>();
                NewsTitleDescription titleDescription = new NewsTitleDescription
                {
                    Title = title,
                    Prompted = output,
                    CreatedAT = @DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                result.Add(titleDescription);
                return result;
            }
        }
        public NewsTitleDescription GetPrompt(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new Exception("Update title, to trigger local model.");
            }
            string result = RunPowerShellCommand(title);
            if (!string.IsNullOrEmpty(result))
            {
                NewsTitleDescription titleDescription = new NewsTitleDescription
                {
                    Title = title,
                    Prompted = result,
                    CreatedAT = @DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                return titleDescription;
            }
            return new NewsTitleDescription();
        }
        private string RunPowerShellCommand(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new Exception("Update title, to trigger local model.");
            }
            string command = $"ollama.exe run dolphin-phi 'May you ask questions based on this news article: {title}?'";
            string result = string.Empty;
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using (Process process = new Process())
            {
                process.StartInfo = psi;
                process.Start();

                result = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Debug.WriteLine($"Error: {error}");
                    return string.Empty;
                }
            }
            return result.Trim();
        }
    }
}