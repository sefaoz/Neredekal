using Neredekal.Hotel.Domain.AggregateModels.HotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Domain.UnitTests.Tests
{
    public class HotelUnitTests
    {
        [Fact]
        public void CreateHotel_ShouldReturnHotelInstance()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var personName = "Sefa";
            var personSurname = "Öz";
            var companyName = "Öz Hotel";
            var contactInfoItems = new List<HotelContactInfoItems>();

            // Act
            var hotel = AggregateModels.HotelModels.Hotel.Create(hotelId, personName, personSurname, companyName, contactInfoItems);

            // Assert
            Assert.NotNull(hotel);
            Assert.Equal(hotelId, hotel.UUID);
            Assert.Equal(personName, hotel.PersonName);
            Assert.Equal(personSurname, hotel.PersonSurname);
            Assert.Equal(companyName, hotel.CompanyName);
            Assert.Empty(hotel.HotelContactInfoItems);
        }

        [Fact]
        public void CreateHotelContactInfoItems_ShouldReturnHotelContactInfoItemsInstance()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var informationType = InformationTypeEnum.Email;
            var informationContent = "sefa.oz@example.com";
            var hotelId = Guid.NewGuid();

            // Act
            var contactInfoItem = HotelContactInfoItems.Create(contactId, informationType, informationContent, hotelId);

            // Assert
            Assert.NotNull(contactInfoItem);
            Assert.Equal(contactId, contactInfoItem.UUID);
            Assert.Equal(informationType, contactInfoItem.InformationType);
            Assert.Equal(informationContent, contactInfoItem.InformationContent);
            Assert.Equal(hotelId, contactInfoItem.HotelId);
        }

        [Fact]
        public void HotelContactInfoItems_ShouldStoreDataCorrectly()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var informationType = InformationTypeEnum.Phone; // Varsayılan bir değer
            var informationContent = "123-456-7890";
            var hotelId = Guid.NewGuid();

            // Act
            var contactInfoItem = HotelContactInfoItems.Create(contactId, informationType, informationContent, hotelId);

            // Assert
            Assert.NotNull(contactInfoItem);
            Assert.Equal("123-456-7890", contactInfoItem.InformationContent);
            Assert.Equal(InformationTypeEnum.Phone, contactInfoItem.InformationType);
        }

        [Fact]
        public void AddMultipleContactInfoItems_ShouldStoreAllItems()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var hotel = AggregateModels.HotelModels.Hotel.Create(hotelId, "Sefa", "Öz", "Öz Hotel", new List<HotelContactInfoItems>());
            var contactInfo1 = HotelContactInfoItems.Create(Guid.NewGuid(), InformationTypeEnum.Email, "email@example.com", hotelId);
            var contactInfo2 = HotelContactInfoItems.Create(Guid.NewGuid(), InformationTypeEnum.Phone, "123-456-7890", hotelId);

            // Act
            hotel.HotelContactInfoItems.Add(contactInfo1);
            hotel.HotelContactInfoItems.Add(contactInfo2);

            // Assert
            Assert.Equal(2, hotel.HotelContactInfoItems.Count);
        }

        [Fact]
        public void CreateHotel_WithNullPersonName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var personSurname = "Öz";
            var companyName = "Öz Hotel";
            var contactInfoItems = new List<HotelContactInfoItems>();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => AggregateModels.HotelModels.Hotel.Create(hotelId, null, personSurname, companyName, contactInfoItems));
            Assert.Equal("personName", exception.ParamName);
        }

        [Fact]
        public void CreateHotelContactInfoItems_WithInvalidInformationType_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var invalidType = (InformationTypeEnum)999; // Geçersiz bir değer
            var informationContent = "contact@example.com";
            var hotelId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => HotelContactInfoItems.Create(contactId, invalidType, informationContent, hotelId));
            Assert.Equal("informationType", exception.ParamName);
        }
    }
}
