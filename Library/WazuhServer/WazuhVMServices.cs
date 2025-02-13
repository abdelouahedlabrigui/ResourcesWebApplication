using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Renci.SshNet;
using ResourcesWebApplication.Models.Devops;

namespace ResourcesWebApplication.Library.WazuhServer
{
    public class WazuhVMServices
    {

        private void RestartWazuhServerService(string ipAddress, string service)
        {
            try
            {
                using (var client = new SshClient(ipAddress, "wazuh-user", "wazuh"))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        string restartCommand = $"sudo systemctl restart {service}";
                        var restartResult = client.RunCommand(restartCommand);

                        // Log the standard output and error output of the restart command
                        if (!string.IsNullOrEmpty(restartResult.Error))
                        {
                            Console.WriteLine($"Error restarting service on {ipAddress}: {restartResult.Error}");
                        }
                        else
                        {
                            Console.WriteLine($"Service {service} restart command issued successfully on {ipAddress}");
                            Console.WriteLine($"Output: {restartResult.Result}");

                            // Check the status of the service after attempting to restart
                            string statusCommand = $"sudo systemctl status {service} --no-pager";
                            var statusResult = client.RunCommand(statusCommand);

                            Console.WriteLine($"Service status output: {statusResult.Result}");

                            if (!string.IsNullOrEmpty(statusResult.Error))
                            {
                                Console.WriteLine($"Error checking service status on {ipAddress}: {statusResult.Error}");
                            }
                            else
                            {
                                Console.WriteLine($"Service {service} status: {statusResult.Result}");
                            }
                        }

                        client.Disconnect();
                    }
                    else
                    {
                        Console.WriteLine($"Failed to connect to {ipAddress}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception message
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }


        public ServiceStatus Restart_WazuhServerService(string ipAddress, string service, string createdAT)
        {
            RestartWazuhServerService(ipAddress, service);
            using (var client = new SshClient(ipAddress, "wazuh-user", "wazuh"))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    string command = $"systemctl status {service}";
                    var output = client.RunCommand(command);
                    client.Disconnect();
                    ServiceStatus result = GetStatusMessage(output.Result, service, ipAddress, createdAT);
                    return result;
                }
                else
                {
                    return new ServiceStatus
                    {
                        IPAddress = ipAddress,
                        Service = service,
                        Status = "SSH connection failed",
                        CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                    };
                }
            }
        }
        private ServiceStatus GetStatusMessage(string statusOutput, string service, string ipAddress, string createdAT)
        {

            // Extract the status from the systemctl output
            var lines = statusOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var activeLine = lines.FirstOrDefault(line => line.Contains("Active:"));

            if (activeLine != null)
            {
                var statusParts = activeLine.Split(new[] { ' ', '(' }, StringSplitOptions.RemoveEmptyEntries);
                if (statusParts.Length > 1)
                {
                    string status = statusParts[1].ToLower();

                    switch (status)
                    {
                        case "active":
                            return new ServiceStatus { IPAddress = ipAddress, Service = service, Status = "Docker service is running.", CreatedAT = createdAT };
                        case "inactive":
                            return new ServiceStatus { IPAddress = ipAddress, Service = service, Status = "Docker service is not running.", CreatedAT = createdAT };
                        case "failed":
                            return new ServiceStatus { IPAddress = ipAddress, Service = service, Status = "Docker service has failed.", CreatedAT = createdAT };
                        case "activating":
                            return new ServiceStatus { IPAddress = ipAddress, Service = service, Status = "Docker service is starting.", CreatedAT = createdAT };
                        case "deactivating":
                            return new ServiceStatus { IPAddress = ipAddress, Service = service, Status = "Docker service is stopping.", CreatedAT = createdAT };
                        default:
                            return new ServiceStatus { IPAddress = ipAddress, Service = service, Status = "Unknown Docker service status.", CreatedAT = createdAT };
                    }
                }
            }

            return new ServiceStatus { IPAddress = ipAddress, Service = service, Status = "Unknown Docker service status.", CreatedAT = createdAT };
        }
    }
}