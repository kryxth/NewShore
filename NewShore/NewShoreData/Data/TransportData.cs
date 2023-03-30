using NewShoreDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewShoreData.Data
{
    public class TransportData
    {
        public int CreateTransport(TransportDTO transport)
        {
            using (var context = new NewShoreBDContext())
            {
                Transport existingTransport = context.Transports.Where(x => x.FlightNumber == transport.FlightNumber && x.FlightCarrier == transport.FlightCarrier).FirstOrDefault();
                int idTransport = 0;
                if (existingTransport != null)
                {
                    Transport newTransport = new Transport();
                    newTransport.FlightCarrier = transport.FlightCarrier;
                    newTransport.FlightNumber = transport.FlightNumber;
                    context.Transports.Add(newTransport);
                    context.SaveChanges();
                    idTransport = newTransport.IdTransport;
                }
                else
                    idTransport = existingTransport.IdTransport;
                return idTransport;
            }
        }
    }
}
