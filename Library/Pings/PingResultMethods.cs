using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Renci.SshNet;
using ResourcesWebApplication.Models.Devops;
using System.Management.Automation;

namespace ResourcesWebApplication.Library.Pings
{
    public class PingResultMethods
    {
        private static readonly string ipv4Pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        private static readonly string ipv6Pattern = @"^((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:|[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|(:[0-9A-Fa-f]{1,4})?:|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:)|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:)|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:)|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)))|(:(:|((:[0-9A-Fa-f]{1,4}){1,7}|:))|((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)))(%.+)?$";
        public ServiceStatus CheckServiceStatusForLinuxDistro(string ipAddress, string username, string password, string service, string createdAT)
        {
            
            using (var client = new SshClient(ipAddress, username, password))
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
        public ServiceStatus CheckWazuhServerServicesStatus(string ipAddress, string service, string createdAT)
        {
            
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
        public PingResult GetUsingPowershellPingResultFromWazuhServerToAgents(string agent)
        {
            string command = $"ping -n 2 {agent}";

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(command);

                var results = ps.Invoke();

                if (results.Count > 0)
                {
                    var result = ParsePingResultForWindowsOSs(string.Join(Environment.NewLine, results.Select(r => r.ToString())), agent);
                    return result;
                }
                else
                {
                    return new PingResult
                    {
                        Status = "Failed",
                        CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                    };
                }
            }
        }

        private PingResult ParsePingResultForWindowsOSs(string result, string ipAddress)
        {
            var lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var summary = lines.FirstOrDefault(line => line.Contains("Packets:"));

            if (summary == null)
            {
                return new PingResult { Status = "Unreachable" };
            }

            // Example of a summary line in Windows ping:
            // Packets: Sent = 4, Received = 4, Lost = 0 (0% loss),
            var parts = summary.Split(new[] { ',', '=' }, StringSplitOptions.RemoveEmptyEntries);
            var transmitted = int.Parse(parts[1].Trim());
            var received = int.Parse(parts[3].Trim());
            var packetLoss = int.Parse(parts[5].Split(' ')[1].Trim());

            var roundTripTimesLine = lines.FirstOrDefault(line => line.Contains("Minimum ="));

            string roundTripTimes = null;
            if (roundTripTimesLine != null)
            {
                var roundTripParts = roundTripTimesLine.Split(new[] { '=', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                roundTripTimes = $"Min/Avg/Max = {roundTripParts[2]}ms/{roundTripParts[5]}ms/{roundTripParts[8]}ms";
            }

            return new PingResult
            {
                IPAddress = ipAddress,
                PacketsTransmitted = transmitted.ToString(),
                PacketsReceived = received.ToString(),
                PacketLoss = packetLoss.ToString(),
                RoundTripTimes = roundTripTimes,
                Status = packetLoss == 100 ? "Unreachable" : "Success",
                CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
            };
        }

        public PingResult GetUsingVMPingResultFromWazuhServerToAgents(string agent, string username, string password)
        {
            using (var client = new SshClient(agent, username, password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    string command = $"ping -c 2 {agent}";
                    var output = client.RunCommand(command);
                    client.Disconnect();
                    var result = ParsePingResult(output.Result, agent);
                    return result;
                } else {
                    return new PingResult();
                }
            }
        }
        public PingResult GetPingResultFromLinuxDistro(string ip, string username, string password)
        {
            using (var client = new SshClient(ip, username, password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    string command = $"ping -c 2 {ip}";
                    var output = client.RunCommand(command);
                    client.Disconnect();
                    var result = ParsePingResult(output.Result, ip);
                    return result;
                } else {
                    return new PingResult();
                }
            }
        }
        public PingResult GetPingResult(string vmIP, string agent_ip, string username, string password)
        {
            using (var client = new SshClient(vmIP, username, password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    string command = $"ping -c 2 {agent_ip}";
                    var output = client.RunCommand(command);
                    client.Disconnect();
                    var result = ParsePingResult(output.Result, agent_ip);
                    return result;
                } else {
                    return new PingResult();
                }
            }
        }
        public PingResult GetPingOfAgentFromVM()
        {
            using (var client = new SshClient("192.168.0.100", "labriguiabdelouahed", "rootroot"))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    string command = $"ping -c 2 192.168.0.117";
                    var output = client.RunCommand(command);
                    client.Disconnect();
                    var result = ParsePingResult(output.Result, "192.168.0.117");
                    return result;
                } else {
                    return new PingResult();
                }
            }
        }
        public PingResult GetPingResultForWindowsOSs(string ipAddress, string username, string password)
        {
            using (var client = new SshClient(ipAddress, username, password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    string command = $"ping -c 2 {ipAddress}";
                    var output = client.RunCommand(command);
                    client.Disconnect();
                    var result = ParsePingResultForWindowsOSs(output.Result, ipAddress);
                    return result;
                } else {
                    return new PingResult();
                }
            }
        }
        

        private PingResult ParsePingResult(string result, string ipAddress)
        {
            var lines = result.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            var summary = lines.LastOrDefault(line => line.Contains("packet loss"));

            if (summary == null)
            {
                return new PingResult {Status = "Unreachable"};
            }

            var parts = summary.Split(',');
            var transmitted = int.Parse(parts[0].Split(' ')[0]);
            var received = int.Parse(parts[1].Split(' ')[1]);
            var packetLoss = int.Parse(parts[2].Split('%')[0].Trim());

            var roundTripTimes = lines.FirstOrDefault(line => line.Contains("rtt min/avg/max/mdev"));

            return new PingResult
            {
                IPAddress = ipAddress,
                PacketsTransmitted = transmitted.ToString(),
                PacketsReceived = received.ToString(),
                PacketLoss = packetLoss.ToString(),
                RoundTripTimes = roundTripTimes,
                Status = packetLoss == 100 ? "Unreachable" : "Success",
                CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
            };
        }
        private bool ICMPPingStatus(string vmIP, string agent_ip, string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            if (Regex.IsMatch(vmIP, ipv4Pattern) && Regex.IsMatch(agent_ip, ipv4Pattern))
            {
                var ping = GetPingResult(vmIP, agent_ip, username, password);
                if (ping.Status == "Success")
                {                    
                    return true;
                } else {
                    return false;
                }
            } else if (Regex.IsMatch(vmIP, ipv6Pattern) && Regex.IsMatch(agent_ip, ipv6Pattern))
            {
                var ping = GetPingResult(vmIP, agent_ip, username, password);
                if (ping.Status == "Success")
                {                    
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
    }
}