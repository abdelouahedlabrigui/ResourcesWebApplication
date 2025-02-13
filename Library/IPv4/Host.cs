using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Library.IPv4
{
    public class Host
    {
        
        public string GenerateRandomIPAddressClass(string which)
        {
            Random random = new Random();
            if (which.Contains("Class A"))
            {
                int part1 = 1;
                int part2 = 127;
                return string.Format("{0}.", random.Next(part1, part2 + 1).ToString());
            } else if (which.Contains("Class B"))
            {
                int part1 = 128;
                int part2 = 191;
                return string.Format("{0}.{1}.", random.Next(part1, part2 + 1).ToString(),random.Next(part1, part2 + 1).ToString());
            } else if (which.Contains("Class C"))
            {
                int part1 = 192;
                int part2 = 223;
                return string.Format("{0}.{1}.{2}.", random.Next(part1, part2 + 1).ToString(),random.Next(part1, part2 + 1).ToString(),random.Next(part1, part2 + 1).ToString());
            } else if (which.Contains("Class D"))
            {
                return "Reserved for multicast";
            } else if (which.Contains("Class E"))
            {
                return "Class E is reserved for experimental, used for research.";
            }
            return "Input may have only Class A, B, C, D or E";
        }
    }
}