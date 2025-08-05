using AutoMapper;
using FluentAssertions;
using HotelListing.Application.Applications;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Mappings;
using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;
using HotelListing.Tests.FakerBuilders.CountryFakerBuilders;
using HotelListing.Tests.FakeRepository;
using Moq;

namespace HotelListing.Tests.UnitTests.Application
{
    public class CountryApplicationTests
    {
        private readonly Mock<ICountryService> _mockCountryService;
        private readonly IMapper _mapper;
        private readonly FakeCountryService _fakeCountryService;

        public CountryApplicationTests()
        {
            _mockCountryService = new Mock<ICountryService>();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<CountryProfile>();
                config.AddProfile<HotelProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _fakeCountryService = new FakeCountryService();
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
            const int ID = 1;
            var existingCountry = new Country { Id = ID, Name = "Pais Antigo", ShortName = "PA" };

            _mockCountryService.Setup(service => service.GetAsync(ID)).ReturnsAsync(existingCountry);
            _mockCountryService.Setup(service => service.UpdateAsync(It.IsAny<Country>())).Returns(Task.CompletedTask);

            // Act
            await application.UpdateAsync(id: ID, updateCountryDto);

            // Assert
            _mockCountryService.Verify(service => service.GetAsync(ID), Times.Once());
            _mockCountryService.Verify(service => service.UpdateAsync(It.Is<Country>(
                c => c.Id == ID &&
                c.Name == updateCountryDto.Name &&
                c.ShortName == updateCountryDto.ShortName
                )));
        }

        [Fact(DisplayName = "Dado país existente, quando remover, então deve remover país e não lançar exceção")]

        public async Task DadoPaisExistenteQuandoRemoverEntaoDeveRemoverPaisENaoLancarExcecao()
        {
            // Arrange
            var application = new CountryApplication(_mockCountryService.Object, _mapper);
            const int ID = 1;

            _mockCountryService.Setup(service => service.DeleteAsync(ID)).Returns(Task.CompletedTask);

            // Act
            await application.DeleteAsync(ID);

            // Assert       
            _mockCountryService.Verify(service => service.DeleteAsync(ID), Times.Once());
        }

        [Fact(DisplayName = "Dado país não existente, quando atualizar, então não deve atualizar país e lançar exceção")]
        public async Task DadoPaisNaoExistenteQuandoAtualizarEntaoNaoDeveAtualizarPaisELancarExcecao()
        {
            // Arrange
            var application = new CountryApplication(_mockCountryService.Object, _mapper);
            var updateCountryDto = UpdateCountryDtoFaker.Generate();
            const int ID = 999;

            _mockCountryService.Setup(service => service.GetAsync(ID)).ThrowsAsync(new NotFoundCustomException(ID.ToString(), "Country"));

            // Act
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await application.UpdateAsync(ID, updateCountryDto);
            });

            // Assert
            _mockCountryService.Verify(service => service.GetAsync(ID), Times.Once());
            _mockCountryService.Verify(service => service.UpdateAsync(It.IsAny<Country>()), Times.Never());
        }

        [Fact(DisplayName = "Dado pais existente, quando buscar, então deve retornar pais buscado ")]
        public async Task DadoPaisExistenteQuandoBuscarEntaoDeveRetornarPaisBuscado()
        {
            // Arrange
            var application = new CountryApplication(_fakeCountryService, _mapper);
            const int ID = 1;
            var expectedCountry = new GetCountryDto
            {
                Id = ID,
                Name = "Brasil",
                ShortName = "BR"
            };

            // Act
            var result = await application.GetAsync(ID);

            //Assert
            result.Should().BeEquivalentTo(expectedCountry);
        }

        [Fact(DisplayName = "Dado pais nao existente, quando buscar, então deve nao deve retornar pais e lançar excessao ")]
        public async Task DadoPaisNaoExistenteQuandoBuscarEntaoNaoDeveRetornarPaisELancarExcessao()
        {
            // Arrange
            var application = new CountryApplication(_fakeCountryService, _mapper);
            const int ID = 999;

            var expectedCountry = new GetCountryDto
            {
                Id = ID,
                Name = "Japão",
                ShortName = "JP"
            };

            // Act     
            Func<Task> act = async () => await application.GetAsync(ID);

            //Assert
            await act.Should().ThrowAsync<NotFoundCustomException>();
        }

        [Fact(DisplayName = "Dado paises existentes, quando buscar, então deve deve retornar paises")]
        public async Task DadoPaisesExistentesQuandoBuscarEntaoDeveRetornarPaises()
        {
            // Arrange
            var application = new CountryApplication(_fakeCountryService, _mapper);
            var expectedCountries = new List<GetCountryDto>
            {
                new GetCountryDto { Id = 1, Name = "Brasil", ShortName = "BR" },
                new GetCountryDto { Id = 2, Name = "Argentina", ShortName = "AR" },
                new GetCountryDto { Id = 3, Name = "Chile", ShortName = "CL" }
            };

            // Act
            var result = await application.GetAllAsync();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(expectedCountries);
        }

        [Fact(DisplayName = "Dado Paises Nulos Ou Vazios, Quando Buscar, Entao Deve Retornar Nulo Ou Vazio")]
        public async Task DadoPaisesNulosOuVaziosQuandoBuscarEntaoDeveRetornarNuloOuVazio()
        {
            // Arrange
            var fakeCountryService = new FakeCountryService(countries: new List<Country>());
            var application = new CountryApplication(fakeCountryService, _mapper);

            // Act
            var result = await application.GetAllAsync();

            // Assert
            result.Should().BeNullOrEmpty();        
        }

        [Fact(DisplayName = "Dado pais existente, quando buscar detalhes, então deve retornar pais buscado com detalhes ")]
        public async Task DadoPaisExistenteQuandoBuscarDetalhesEntaoDeveRetornarPaisBuscadoComDetalhes()
        {
            // Arrange
            var application = new CountryApplication(_fakeCountryService, _mapper);
            const int ID = 2;
            const int HOTEL_ID = 3;

            var expectedCountryDetails = new GetCountryDetailsDto
            {
                Id = ID,
                Name = "Argentina",
                ShortName = "AR",
                Hotels = new List<GetHotelDto> {
                    new GetHotelDto
                    {
                       Id = HOTEL_ID, Name = "Pousada Sol", Address = "Rua C, 789", Rating = 4.0, CountryId = ID
                    }
                }
            };

            // Act
            var result = await application.GetDetailsAsync(ID);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedCountryDetails);
        }
    }
}
