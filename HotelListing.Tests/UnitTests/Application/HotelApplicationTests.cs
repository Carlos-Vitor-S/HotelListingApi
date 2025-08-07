using AutoMapper;
using FluentAssertions;
using HotelListing.Application.Applications;
using HotelListing.Application.DTOs.HotelDTOs;
using HotelListing.Application.Mappings;
using HotelListing.Application.Models;
using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Interfaces.IServices;
using HotelListing.Domain.Models;
using HotelListing.Tests.FakeApplications;
using HotelListing.Tests.FakerBuilders.HotelFakerBuilders;
using HotelListing.Tests.FakeServices;
using Moq;

namespace HotelListing.Tests.UnitTests.Application
{
    public class HotelApplicationTests
    {
        private readonly Mock<IHotelService> _mockHotelService;
        private readonly IMapper _mapper;
        private readonly FakerHotelService _fakeHotelService;

        public HotelApplicationTests()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<HotelProfile>();

            });
            _mapper = mapperConfig.CreateMapper();
            _mockHotelService = new Mock<IHotelService>();
            _fakeHotelService = new FakerHotelService();
        }

        [Fact(DisplayName = "Dado hotel válido, quando cadastrar, então deve cadastrar hotel e nao lancar exceção")]
        public async Task DadoHotelValidoQuandoCadastrarEntaoDeveCadastrarHotelEnaoLancarExcecao()
        {
            // Arrange
            var application = new HotelApplication(_mockHotelService.Object, _mapper);
            var expectedHotelDto = CreateHotelDtoFaker.Generate();
            expectedHotelDto.CountryId = 1;


            _mockHotelService.Setup(service => service.CreateAsync(It.IsAny<Hotel>())).Returns(Task.CompletedTask);

            // Act
            Func<Task> act = () => application.CreateAsync(expectedHotelDto);

            // Assert
            await act.Should().NotThrowAsync();
            _mockHotelService.Verify(service => service.CreateAsync(It.Is<Hotel>(h =>
                h.Name == expectedHotelDto.Name &&
                h.Address == expectedHotelDto.Address &&
                h.Rating == expectedHotelDto.Rating &&
                h.CountryId == expectedHotelDto.CountryId
            )), Times.Once());
        }

        [Fact(DisplayName = "Dado hotel existente, quando remover, então deve remover hotel e não lançar exceção")]

        public async Task DadoHotelExistenteQuandoRemoverEntaoDeveRemoverHotelENaoLancarExcecao()
        {
            // Arrange
            var application = new HotelApplication(_mockHotelService.Object, _mapper);
            const int ID = 1;

            _mockHotelService.Setup(service => service.ExistsAsync(ID)).ReturnsAsync(true);
            _mockHotelService.Setup(service => service.DeleteAsync(ID)).Returns(Task.CompletedTask);

            // Act
            Func<Task> act = () => application.DeleteAsync(ID);

            // Assert
            await act.Should().NotThrowAsync();
            _mockHotelService.Verify(service => service.DeleteAsync(ID), Times.Once());
        }


        [Fact(DisplayName = "Dado hotel nao existente, quando remover, então não deve remover hotel e lançar exceção")]

        public async Task DadoHotelNaoExistenteQuandoRemoverEntaoNaoDeveRemoverHotelELancarExcecao()
        {
            // Arrange
            var application = new HotelApplication(_mockHotelService.Object, _mapper);
            const int ID = 9999;

            _mockHotelService.Setup(service => service.DeleteAsync(ID)).ThrowsAsync(new NotFoundCustomException(ID.ToString(), "Hotel"));

            Func<Task> act = () => application.DeleteAsync(ID);

            // Assert       
            await act.Should().ThrowAsync<NotFoundCustomException>();
            _mockHotelService.Verify(service => service.DeleteAsync(ID), Times.Once());
        }

        [Fact(DisplayName = "Dado hotel não existente, quando atualizar, então não deve atualizar hotel e lançar exceção")]
        public async Task DadoHotelNaoExistenteQuandoAtualizarEntaoNaoDeveAtualizarHotelELancarExcecao()
        {
            // Arrange
            var application = new HotelApplication(_mockHotelService.Object, _mapper);
            var updateHotelDto = UpdateHotelDtoFaker.Generate();
            const int ID = 999;

            _mockHotelService.Setup(service => service.GetAsync(ID)).ThrowsAsync(new NotFoundCustomException(ID.ToString(), "Hotel"));

            // Act

            Func<Task> act = () => application.UpdateAsync(ID, updateHotelDto);

            // Assert
            await act.Should().ThrowAsync<NotFoundCustomException>();
            _mockHotelService.Verify(service => service.GetAsync(ID), Times.Once());
            _mockHotelService.Verify(service => service.UpdateAsync(It.IsAny<Hotel>()), Times.Never());
        }

        [Fact(DisplayName = "Dado hotel existente, quando buscar, então deve retornar hotel buscado ")]
        public async Task DadoHotelExistenteQuandoBuscarEntaoDeveRetornarHotelBuscado()
        {
            // Arrange
            var application = new HotelApplication(_fakeHotelService, _mapper);
            const int ID = 1;

            var expectedHotelDto = new GetHotelDto
            {
                Id = ID,
                Name = "Hotel Nossa Senhora da Gloria",
                Address = "231 Rua das Rosas, Nossa Senhora da Gloria, SE",
                Rating = 5,
                CountryId = 5
            };

            // Act
            var act = await application.GetAsync(ID);

            //Assert
            act.Should().BeEquivalentTo(expectedHotelDto);
        }

        [Fact(DisplayName = "Dado hotel nao existente, quando buscar, então deve nao deve retornar hotel e lançar exceção ")]
        public async Task DadoHotelNaoExistenteQuandoBuscarEntaoNaoDeveRetornarHotelELancarExcecao()
        {
            // Arrange
            var application = new HotelApplication(_fakeHotelService, _mapper);
            const int ID = 999;

            var expectedHotelDto = new GetHotelDto
            {
                Id = ID,
                Name = "Hotel Nossa Senhora da Gloria",
                Address = "231 Rua das Rosas, Nossa Senhora da Gloria, SE",
                Rating = 5,
                CountryId = 5
            };

            // Act     
            Func<Task> act = () => application.GetAsync(ID);

            //Assert
            await act.Should().ThrowAsync<NotFoundCustomException>();
        }

        [Fact(DisplayName = "Dado Hoteis existentes, quando buscar, então deve deve retornar Hoteis")]
        public async Task DadoHoteisExistentesQuandoBuscarEntaoDeveRetornarHoteis()
        {
            // Arrange
            var application = new HotelApplication(_fakeHotelService, _mapper);
            var expectedHotels = new List<GetHotelDto>
            {
               new GetHotelDto
                {
                    Id = 1,
                    Name ="Hotel Nossa Senhora da Gloria",
                    Address ="231 Rua das Rosas, Nossa Senhora da Gloria, SE",
                    Rating = 5,
                    CountryId = 5
                },

                new GetHotelDto
                {
                    Id = 14,
                    Name = "Hotel Paradise",
                    Address = "123 Rua das Flores, São Paulo, SP",
                    Rating = 4.5,
                    CountryId = 18
                },

                new GetHotelDto
                {
                    Id = 7,
                    Name = "Hotel x",
                    Address = "Rua Y",
                    Rating = 3.5,
                    CountryId = 6
                }
            };

            // Act
            var act = await application.GetAllAsync();

            // Assert
            act.Should().NotBeNullOrEmpty();
            act.Should().BeEquivalentTo(expectedHotels);
        }

        [Fact(DisplayName = "Dado Hoteis Nulos Ou Vazios, Quando Buscar, Entao Deve Retornar Nulo Ou Vazio")]
        public async Task DadoHoteisNulosOuVaziosQuandoBuscarEntaoDeveRetornarNuloOuVazio()
        {
            // Arrange
            var fakeHotelService = new FakerHotelService(hotels: new List<Hotel>());
            var application = new HotelApplication(fakeHotelService, _mapper);

            // Act
            var act = await application.GetAllAsync();

            // Assert
            act.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "Dado Hoteis existentes, quando paginar, então deve retornar apenas a página solicitada")]

        public async Task DadoHoteisExistentesQuandoPaginarEntaoDeveRetornarPaginaSolicitada()
        {
            // Arrange
            var applicationFake = new FakerHotelApplication();
            const int PAGE_SIZE = 1;
            const int PAGE_NUMBER = 3;
            var hotels = new List<Hotel> {
                new Hotel
                {
                    Id = 1,
                    Name ="Hotel Nossa Senhora da Gloria",
                    Address ="231 Rua das Rosas, Nossa Senhora da Gloria, SE",
                    Rating = 5,
                    CountryId = 5
                },

                new Hotel
                {
                    Id = 14,
                    Name = "Hotel Paradise",
                    Address = "123 Rua das Flores, São Paulo, SP",
                    Rating = 4.5,
                    CountryId = 18
                },

                new Hotel
                {
                    Id = 7,
                    Name = "Hotel x",
                    Address = "Rua Y",
                    Rating = 3.5,
                    CountryId = 6
                } };

            var paginationParameters = new PaginationParameters
            {
                PageSize = PAGE_SIZE,
                PageNumber = PAGE_NUMBER,
            };

            var expectedHotelsDto = new List<GetHotelDto>
            {
                new GetHotelDto {
                    Id = 7,
                    Name = "Hotel x",
                    Address = "Rua Y",
                    Rating = 3.5,
                    CountryId = 6 }
            };

            _mockHotelService.Setup(service => service.GetAllAsQueryable()).Returns(hotels.AsQueryable());

            // Act
            var act = await applicationFake.GetAllByPageAsync(paginationParameters);

            // Assert
            act.Should().NotBeNull();
            act.CurrentPage.Should().Be(PAGE_NUMBER);
            act.PageSize.Should().Be(PAGE_SIZE);
            act.Items.Should().BeEquivalentTo(expectedHotelsDto);
        }

        [Fact(DisplayName = "Dado Hoteis Existentes Quando Paginar Nao Existir Entao Deve Retornar Pagina Vazia")]

        public async Task DadoHoteisExistentesQuandoPaginarNaoExistirEntaoDeveRetornarPaginaVazia()
        {
            // Arrange
            var applicationFake = new FakerHotelApplication();
            const int PAGE_SIZE = 1;
            const int PAGE_NUMBER = 4;
            var hotels = new List<Hotel> {
                new Hotel
                {
                    Id = 1,
                    Name ="Hotel Nossa Senhora da Gloria",
                    Address ="231 Rua das Rosas, Nossa Senhora da Gloria, SE",
                    Rating = 5,
                    CountryId = 5
                },

                new Hotel
                {
                    Id = 14,
                    Name = "Hotel Paradise",
                    Address = "123 Rua das Flores, São Paulo, SP",
                    Rating = 4.5,
                    CountryId = 18
                },

                new Hotel
                {
                    Id = 7,
                    Name = "Hotel x",
                    Address = "Rua Y",
                    Rating = 3.5,
                    CountryId = 6
                } };

            var paginationParameters = new PaginationParameters
            {
                PageSize = PAGE_SIZE,
                PageNumber = PAGE_NUMBER,
            };

            _mockHotelService.Setup(service => service.GetAllAsQueryable()).Returns(hotels.AsQueryable());

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
