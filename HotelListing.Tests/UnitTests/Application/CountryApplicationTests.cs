using AutoMapper;
using HotelListing.Application.Applications;
using HotelListing.Application.Mappings;
using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;
using HotelListing.Tests.FakerBuilders.CountryFakerBuilders;
using Moq;

namespace HotelListing.Tests.UnitTests.Application
{
    public class CountryApplicationTests
    {
        private Mock<ICountryService> _mockCountryService;
        private IMapper _mapper;
        public CountryApplicationTests()
        {
            _mockCountryService = new Mock<ICountryService>();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<CountryProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact(DisplayName = "Dado país válido, quando cadastrar, então deve cadastrar país e nao lancar exceção")]
        public async Task DadoPaisValidoQuandoCadastrarEntaoDeveCadastrarPaisEnaoLancarExcecao()
        {
            // Arrange
            var application = new CountryApplication(_mockCountryService.Object, _mapper);
            var expectedCountry = CreateCountryDtoFaker.Generate();

            _mockCountryService.Setup(service => service.CreateAsync(It.IsAny<Country>())).Returns(Task.CompletedTask);

            // Act
            await application.CreateAsync(expectedCountry);

            // Assert
            _mockCountryService.Verify(service => service.CreateAsync(It.Is<Country>(c =>
                c.Name == expectedCountry.Name &&
                c.ShortName == expectedCountry.ShortName

            )), Times.Once());
        }

        [Fact(DisplayName = "Dado país existente, quando atualizar, então deve atualizar país e não lançar exceção")]

        public async Task DadoPaisExistenteQuandoAtualizarEntaoDeveAtualizarPaisENaoLancarExcecao()
        {
            // Arrange
            var application = new CountryApplication(_mockCountryService.Object, _mapper);
            var updateCountryDto = UpdateCountryDtoFaker.Generate();

            int id = 1;
            var existingCountry = new Country { Id = id, Name = "Pais Antigo", ShortName = "PA" };

            _mockCountryService.Setup(service => service.Get(id)).ReturnsAsync(existingCountry);
            _mockCountryService.Setup(service => service.UpdateAsync(It.IsAny<Country>())).Returns(Task.CompletedTask);

            // Act
            await application.UpdateAsync(id: id, updateCountryDto);

            // Assert
            _mockCountryService.Verify(service => service.Get(id), Times.Once());
            _mockCountryService.Verify(service => service.UpdateAsync(It.Is<Country>(
                c => c.Id == id &&
                c.Name == updateCountryDto.Name &&
                c.ShortName == updateCountryDto.ShortName
                )));
        }

        [Fact(DisplayName = "Dado país existente, quando remover, então deve remover país e não lançar exceção")]

        public async Task DadoPaisExistenteQuandoRemoverEntaoDeveRemoverPaisENaoLancarExcecao()
        {
            // Arrange
            var application = new CountryApplication(_mockCountryService.Object, _mapper);
            int id = 1222222;
                      
            _mockCountryService.Setup(service => service.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await application.DeleteAsync(id);

            // Assert
          
            _mockCountryService.Verify(service => service.DeleteAsync(id), Times.Once());
        }

        [Fact(DisplayName = "Dado país inexistente, quando atualizar, então não deve atualizar país e lançar exceção")]
        public async Task DadoPaisInexistenteQuandoAtualizarEntaoNaoDeveAtualizarPaisELancarExcecao()
        {
            // Arrange
            var application = new CountryApplication(_mockCountryService.Object, _mapper);
            var updateCountryDto = UpdateCountryDtoFaker.Generate();

            int id = 999;
            _mockCountryService.Setup(service => service.Get(id)).ThrowsAsync(new NotFoundCustomException(id.ToString(), "Country"));
            // Act
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await application.UpdateAsync(id, updateCountryDto);
            });

            // Assert
            _mockCountryService.Verify(service => service.Get(id), Times.Once());
            _mockCountryService.Verify(service => service.UpdateAsync(It.IsAny<Country>()), Times.Never());
        }



    }
}
