using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.API.Controllers;
using Solution.API.DataModels;
using Solution.DAL.EF;
using Solution.DAL.Repository;

namespace UnitTestAPI
{
    public class CategoriasControllerTest
    {
        private readonly SolutionDbContext _context;
        private readonly Repository<Categorias> _repository;
        private readonly CategoriasController _controller;

        public CategoriasControllerTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SolutionDbContext>();
            optionsBuilder.UseSqlServer("ConnectionStringDeTuBaseDeDatos"); 
            _context = new SolutionDbContext(optionsBuilder.Options);
            _repository = new Repository<Categorias>(_context);
            _controller = new CategoriasController(_repository);
        }

        [Fact]
        public async Task GetCategorias_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetCategorias();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task GetCategoria_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _controller.GetCategoria(id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}