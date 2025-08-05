using AutoMapper;
using FluentAssertions;
using HotelListing.Application.Applications;
using HotelListing.Application.DTOs.CountryDTOs;
using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Mappings;
using HotelListing.Application.Models;
using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;
using HotelListing.Tests.FakeApplications;
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
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<CountryProfile>();
                config.AddProfile<HotelProfile>();

            });
            _mapper = mapperConfig.CreateMapper();
            _mockCountryService = new Mock<ICountryService>();
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
            Func<Task> act = () => application.CreateAsync(expectedCountry);
            
            // Assert
            await act.Should().NotThrowAsync();
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
            Func<Task> act = () => application.UpdateAsync(ID, updateCountryDto);

            // Assert
            await act.Should().NotThrowAsync();
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

            _mockCountryService.Setup(service => service.ExistsAsync(ID)).ReturnsAsync(true);
            _mockCountryService.Setup(service => service.DeleteAsync(ID)).Returns(Task.CompletedTask);

            // Act
            Func<Task> act = () => application.DeleteAsync(ID);

            // Assert
            await act.Should().NotThrowAsync();
            _mockCountryService.Verify(service => service.DeleteAsync(ID), Times.Once());
        }

        [Fact(DisplayName = "Dado país nao existente, quando remover, então não deve remover país e lançar exceção")]

        public async Task DadoPaisNaoExistenteQuandoRemoverEntaoNaoDeveRemoverPaisELancarExcecao()
        {
            // Arrange
            var application = new CountryApplication(_mockCountryService.Object, _mapper);
            const int ID = 9999;

            _mockCountryService.Setup(service => service.DeleteAsync(ID)).ThrowsAsync(new NotFoundCustomException(ID.ToString(), "Country"));

            Func<Task> act = () => application.DeleteAsync(ID);

            // Assert       
            await act.Should().ThrowAsync<NotFoundCustomException>();
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

            Func<Task> act = () => application.UpdateAsync(ID , updateCountryDto);

            // Assert
            await act.Should().ThrowAsync<NotFoundCustomException>();
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
            var act = await application.GetAsync(ID);

            //Assert
            act.Should().BeEquivalentTo(expectedCountry);
        }

        [Fact(DisplayName = "Dado pais nao existente, quando buscar, então deve nao deve retornar pais e lançar exceção ")]
        public async Task DadoPaisNaoExistenteQuandoBuscarEntaoNaoDeveRetornarPaisELancarExcecao()
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
            Func<Task> act = () => application.GetAsync(ID);

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
            var act = await application.GetAllAsync();

            // Assert
            act.Should().NotBeNullOrEmpty();
            act.Should().BeEquivalentTo(expectedCountries);
        }

        [Fact(DisplayName = "Dado Paises Nulos Ou Vazios, Quando Buscar, Entao Deve Retornar Nulo Ou Vazio")]
        public async Task DadoPaisesNulosOuVaziosQuandoBuscarEntaoDeveRetornarNuloOuVazio()
        {
            // Arrange
            var fakeCountryService = new FakeCountryService(countries: new List<Country>());
            var application = new CountryApplication(fakeCountryService, _mapper);

            // Act
            var act = await application.GetAllAsync();

            // Assert
            act.Should().BeNullOrEmpty();
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
            var act = await application.GetDetailsAsync(ID);

            // Assert
            act.Should().NotBeNull();
            act.Should().BeEquivalentTo(expectedCountryDetails);
        }

        [Fact(DisplayName = "Dado Pais Nao Existente Quando Buscar Detalhes Entao Nao Deve Retornar Pais Buscado Com Detalhes E Lancar Exceção")]
        public async Task DadoPaisNaoExistenteQuandoBuscarDetalhesEntaoNaoDeveRetornarPaisBuscadoComDetalhesELancarExcecao()
        {
            // Arrange
            var application = new CountryApplication(_fakeCountryService, _mapper);
            const int ID = 9999;

            // Act
            Func<Task> act = async () => await application.GetDetailsAsync(ID);

            // Assert
            await act.Should().ThrowAsync<NotFoundCustomException>();
        }

        [Fact(DisplayName = "Dado países existentes, quando paginar, então deve retornar apenas a página solicitada")]

        public async Task DadoPaisesExistentesQuandoPaginarEntaoDeveRetornarPaginaSolicitada()
        {
            // Arrange
            var applicationFake = new FakeCountryApplication();
            const int PAGE_SIZE = 1;
            const int PAGE_NUMBER = 3;
            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "Brasil", ShortName = "BR" },
                new Country { Id = 2, Name = "Argentina", ShortName = "AR" },
                new Country { Id = 3, Name = "Chile", ShortName = "CL" }
            };
            var expectedCountriesDto = new List<GetCountryDto>
            {
                new GetCountryDto { Id = 3, Name = "Chile", ShortName = "CL" }
            };
            var paginationParameters = new PaginationParameters
            {
                PageSize = PAGE_SIZE,
                PageNumber = PAGE_NUMBER,
            };

            _mockCountryService.Setup(service => service.GetAllAsQueryable()).Returns(countries.AsQueryable());

            // Act
            var act = await applicationFake.GetAllByPageAsync(paginationParameters);

            // Assert
            act.Should().NotBeNull();
            act.CurrentPage.Should().Be(PAGE_NUMBER);
            act.PageSize.Should().Be(PAGE_SIZE);
            act.Items.Should().BeEquivalentTo(expectedCountriesDto);
        }

        [Fact(DisplayName = "Dado Países Existentes Quando Paginar Nao Existir Entao Deve Retornar Pagina Vazia")]

        public async Task DadoPaisesExistentesQuandoPaginarNaoExistirEntaoDeveRetornarPaginaVazia()
        {
            // Arrange
            var applicationFake = new FakeCountryApplication();
            const int PAGE_SIZE = 1;
            const int PAGE_NUMBER = 100;
            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "Brasil", ShortName = "BR" },
                new Country { Id = 2, Name = "Argentina", ShortName = "AR" },
                new Country { Id = 3, Name = "Chile", ShortName = "CL" }
            };
            var paginationParameters = new PaginationParameters
            {
                PageSize = PAGE_SIZE,
                PageNumber = PAGE_NUMBER,
            };

            _mockCountryService.Setup(service => service.GetAllAsQueryable()).Returns(countries.AsQueryable());

            // Act
            var act = await applicationFake.GetAllByPageAsync(paginationParameters);

            // Assert
            act.Should().NotBeNull();
            act.Items.Should().BeEmpty();
            act.CurrentPage.Should().Be(PAGE_NUMBER);
            act.PageSize.Should().Be(PAGE_SIZE);
        }
    }
}
