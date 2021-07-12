using CQRS.Business.Commands.Requests;
using CQRS.Business.Commands.Responses;
using CQRS.Business.Handlers.Concrete;
using CQRS.Domain.Entities;
using CQRS.Domain.Models.ValidationAtributes;
using CQRS.Domain.Repository;
using CQRS.Infra.Context;
using ExpectedObjects;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CQRS.Testes {
    public class TestesUnitarios {
        

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestExceptionTitleEmptyOrNull(string titleInvalido) {

            Assert.Throws<ArgumentException>(() => new Todo(titleInvalido , "TesteDescrição" , true));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestExceptionDescriptionEmptyOrNull(string descInvalido) {

            Assert.Throws<ArgumentException>(() => new Todo("testando", descInvalido ,false));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestExceptionDateAndComplete(bool complete) {

            Assert.Throws<ArgumentException>(() => new Todo("testando", "testesData", complete));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Jade")]
        public void TestExceptionTitleShortOrEmpty(string title) {

            Assert.Throws<ArgumentException>(() => new Todo(title, "testesTitle", true ));
        }

        [Theory]
        [InlineData("")]
        [InlineData("teste")]
        public void TestExceptionDescriptionShortOrEmpty(string Description) {

            Assert.Throws<ArgumentException>(() => new Todo("testando", Description,true));
        }

        [Fact]
        public void TestExceptionAllNull() {

            Assert.Throws<ArgumentException>(() => new Todo(null, null , false));
        }


        [Fact]
        public void TestValidationException() {
            ValidationShort valida = new();
            Assert.False(valida.IsValid("teste"));
        }

        [Fact]
        public void TestCreateRequest() {


            var CreateEsperado = new {
                Title = "Teste",
                Description = "TesteDescrição",
                Complete = false
            };

            CreateTodoRequest teste = new(CreateEsperado.Title, CreateEsperado.Description, CreateEsperado.Complete);

            CreateEsperado.ToExpectedObject().ShouldMatch(teste);

        }

        [Fact]
        public void TestUpdateRequest() {


            var UpdateEsperado = new {
                Id = 1,
                Complete = false
            };

            UpdateTodoRequest teste = new(1);
            UpdateEsperado.ToExpectedObject().ShouldMatch(teste);

        }

        [Fact]
        public void TestUpdateDescRequest() {


            var UpdateDescEsperado = new {
                Id = 1,
                Description = "teste"
            };

            UpdateDescTodoRequest teste = new(1, "teste");

            UpdateDescEsperado.ToExpectedObject().ShouldMatch(teste);

        }

        [Fact]
        public void TestDeleteRequest() {


            var UpdateDeleteEsperado = new {
                Id = 1
            };

            DeleteTodoRequest teste = new(1);

            UpdateDeleteEsperado.ToExpectedObject().ShouldMatch(teste);

        }

        [Fact]
        public void TestRespose() {


            var UpdateDeleteEsperado = new {
                 Id = 1,
                 Title = "teste",
                 Complete = true,
                 DateComplete = DateTime.Now,
                 Description = "testando"
            };

            TodosResponse teste = new() {
                Id = UpdateDeleteEsperado.Id,
                Title = UpdateDeleteEsperado.Title,
                Complete = UpdateDeleteEsperado.Complete,
                DateComplete = UpdateDeleteEsperado.DateComplete,
                Description = UpdateDeleteEsperado.Description
            };

            UpdateDeleteEsperado.ToExpectedObject().ShouldMatch(teste);

        }

        [Fact]
        public void TestTodo() {

            var Todo = new {
                Id = 0,
                Title = "testado",
                Complete = true,
                DateComplete = DateTime.Now,
                Description = "testando"
            };

            Todo teste = new(Todo.Title, Todo.Description , Todo.Complete);

            Todo.ToExpectedObject().ShouldMatch(teste);

        }

    }
}
