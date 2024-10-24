using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.SeedWorks;

namespace Neredekal.Hotel.Domain.AggregateModels.HotelModels
{
    public class HotelContactInfoItems : Entity
    {
        #region properties
        public override Guid UUID { get; set; }
        public InformationTypeEnum InformationType { get; set; }
        public string InformationContent { get; set; }
        #endregion

        #region navigtion properties
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        #endregion

        #region ctors
        protected HotelContactInfoItems() { }

        public HotelContactInfoItems(Guid id, InformationTypeEnum informationType, string informationContent, Guid hotelId)
        {
            UUID = id;
            InformationType = informationType;
            InformationContent = informationContent;
            HotelId = hotelId;
        }
        #endregion

        #region methods

        public static HotelContactInfoItems Create(Guid id, InformationTypeEnum informationType, string informationContent,
            Guid hotelId)
        {
            return new(id, informationType, informationContent, hotelId);
        }

        #endregion
    }
}
