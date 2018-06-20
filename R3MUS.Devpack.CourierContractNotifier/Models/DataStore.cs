using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.Devpack.CourierContractNotifier.Models
{
    public class DataStore
    {
        private const string _unknownStatus = "unknown";

        public List<DataItem> Data { get; set; }

        public void CheckContracts(IEnumerable<long> ids)
        {
            Data.RemoveAll(w => !ids.Contains(w.Id));
            ids.ToList().ForEach(id => {
                if (!Data.Select(s => s.Id).Contains(id))
                {
                    Data.Add(new DataItem() { Id = id, Status = _unknownStatus });
                }
            });
        }

        public bool DoNotification(long contractId, string status)
        {
            var item = Data.FirstOrDefault(w => w.Id == contractId);
            var result = !status.Equals(item.Status);
            item.Status = status;

            return result;
        }
    }

    public class DataItem
    {
        public long Id { get; set; }
        public string Status { get; set; }
    }
}
