using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.SeedWorks;

namespace Neredekal.Hotel.Domain.AggregateModels.HotelModels
{
    public class Hotel : AggregateRoot
    {
        #region properties
        public override Guid UUID { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string CompanyName { get; set; }
        #endregion

        #region navigation properties
        public List<HotelContactInfoItems> HotelContactInfoItems { get; set; }
        #endregion

        #region ctor
        protected Hotel() { }

        private Hotel(Guid id, string personName, string personSurname, string companyName, List<HotelContactInfoItems> hotelContactInfoItems)
        {
            UUID = id;
            PersonName = personName;
            PersonSurname = personSurname;
            CompanyName = companyName;
            HotelContactInfoItems = new List<HotelContactInfoItems>();
            HotelContactInfoItems.AddRange(hotelContactInfoItems);
        }
        #endregion

        #region methods

        public static Hotel Create(Guid id, string personName, string personSurname, string companyName, List<HotelContactInfoItems> hotelContactInfoItems)
        {
            return new (id,personName, personSurname, companyName, hotelContactInfoItems);
        }
        #endregion
    }
}
