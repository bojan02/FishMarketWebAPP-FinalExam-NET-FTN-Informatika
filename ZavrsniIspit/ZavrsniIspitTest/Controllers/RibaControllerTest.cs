using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZavrsniIspit.Controllers;
using ZavrsniIspit.Interfaces;
using ZavrsniIspit.Models;

namespace ZavrsniIspitTest.Controllers
{
    public class RibaControllerTest
    {
        [Fact]
        public void GetRiba_ValidId_ReturnsObject()
        {
            // Arrange
            Riba riba = new Riba()
            {
                Id = 1,
                Sorta = "Smudj",
                MestoUlova = "Ribnjak Bager",
                Cena = 1100,
                DostupnaKolicina = 20,
                RibarnicaId = 3,
                Ribarnica = new Ribarnica { Id = 1, Naziv = "Dunav doo", GodinaOtvaranja = 2015 }
            };


            var mockRepository = new Mock<IRibaRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(riba);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new RibaProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new RibeController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.GetRiba(1) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            RibaDetailDTO dtoResult = (RibaDetailDTO)actionResult.Value;
            Assert.Equal(riba.Id, dtoResult.Id);
            Assert.Equal(riba.Sorta, dtoResult.Sorta);
            Assert.Equal(riba.Cena, dtoResult.Cena);
            Assert.Equal(riba.MestoUlova, dtoResult.MestoUlova);
            Assert.Equal(riba.DostupnaKolicina, dtoResult.DostupnaKolicina);
            Assert.Equal(riba.RibarnicaId, dtoResult.RibarnicaId);
            Assert.Equal(riba.Ribarnica.Naziv, dtoResult.RibarnicaNaziv);
            Assert.Equal(riba.Ribarnica.GodinaOtvaranja, dtoResult.RibarnicaGodinaOtvaranja);

        }

        [Fact]
        public void PutRiba_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            Riba riba = new Riba()
            {
                Id = 1,
                Sorta = "Smudj",
                MestoUlova = "Ribnjak Bager",
                Cena = 1100,
                DostupnaKolicina = 20,
                RibarnicaId = 3,
                Ribarnica = new Ribarnica { Id = 1, Naziv = "Dunav doo", GodinaOtvaranja = 2015 }
            };

            var mockRepository = new Mock<IRibaRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new RibaProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new RibeController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PutRiba(24, riba) as BadRequestResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void DeleteRiba_ValidId_ReturnsOkResult()
        {
            // Arrange
            Riba riba = new Riba()
            {
                Id = 1,
                Sorta = "Smudj",
                MestoUlova = "Ribnjak Bager",
                Cena = 1100,
                DostupnaKolicina = 20,
                RibarnicaId = 3,
                Ribarnica = new Ribarnica { Id = 1, Naziv = "Dunav doo", GodinaOtvaranja = 2015 }
            };

            var mockRepository = new Mock<IRibaRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(riba);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new RibaProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new RibeController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.DeleteRiba(1) as OkResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void DeleteRiba_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IRibaRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new RibaProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new RibeController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.DeleteRiba(12) as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void Search_ReturnsOk()
        {
            // Arrange
            List<Riba> ribe = new List<Riba>() {
                new Riba()  {
                    Id = 1,
                    Sorta = "Smudj",
                    MestoUlova = "Ribnjak Bager",
                    Cena = 1100,
                    DostupnaKolicina = 20,
                    RibarnicaId = 1,
                    Ribarnica = new Ribarnica { Id = 1, Naziv = "Tisa str", GodinaOtvaranja = 2012 }
                },
                new Riba()  {
                    Id = 2,
                    Sorta = "Saran",
                    MestoUlova = "Dunav",
                    Cena = 860,
                    DostupnaKolicina = 30,
                    RibarnicaId = 2,
                    Ribarnica = new Ribarnica { Id = 2, Naziv = "Dunav doo", GodinaOtvaranja = 2015 }
                }
            };

            PretragaDTO searchDTO = new PretragaDTO()
            {
                Najmanje = 1,
                Najvise = 31
            };

            var mockRepository = new Mock<IRibaRepository>();
            mockRepository.Setup(x => x.GetAllByParameters(searchDTO.Najmanje, searchDTO.Najmanje)).Returns(ribe.AsQueryable());

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new RibaProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new RibeController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.Search(searchDTO) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            List<RibaDetailDTO> listResult = (List<RibaDetailDTO>)actionResult.Value;


            for (int i = 0; i < listResult.Count; i++)
            {
                Assert.Equal(ribe[i].Id, listResult[i].Id);
                Assert.Equal(ribe[i].Sorta, listResult[i].Sorta);
                Assert.Equal(ribe[i].Cena, listResult[i].Cena);
                Assert.Equal(ribe[i].MestoUlova, listResult[i].MestoUlova);
                Assert.Equal(ribe[i].DostupnaKolicina, listResult[i].DostupnaKolicina);
                Assert.Equal(ribe[i].RibarnicaId, listResult[i].RibarnicaId);
                Assert.Equal(ribe[i].Ribarnica.Naziv, listResult[i].RibarnicaNaziv);
                Assert.Equal(ribe[i].Ribarnica.GodinaOtvaranja, listResult[i].RibarnicaGodinaOtvaranja);
            }
        }
    }
}
