using CQRS.Business.Commands.Requests;
using CQRS.Business.Commands.Responses;
using CQRS.Business.Handlers.Concrete;
using CQRS.Domain.Entities;
using CQRS.Domain.Models.ValidationAtributes;
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

            Assert.Throws<ArgumentException>(() => new Todo(titleInvalido, true,
                        DateTime.Now, "TesteDescrição"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestExceptionDescriptionEmptyOrNull(string descInvalido) {

            Assert.Throws<ArgumentException>(() => new Todo("testando", false,
                        DateTime.Now, descInvalido));
        }

        [Theory]
        [InlineData(true , null)]
        [InlineData(false , "01/06/2021")]
        public void TestExceptionDateAndComplete(bool complete , string data) {

            Assert.Throws<ArgumentException>(() => new Todo("testando", complete,
                        data == null ? null : Convert.ToDateTime(data), "testesData"));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Jade")]
        public void TestExceptionTitleShortOrEmpty(string title) {

            Assert.Throws<ArgumentException>(() => new Todo(title, true,
                        DateTime.Now, "testesTitle"));
        }

        [Theory]
        [InlineData("")]
        [InlineData("teste")]
        public void TestExceptionDescriptionShortOrEmpty(string Description) {

            Assert.Throws<ArgumentException>(() => new Todo("testando", true,
                        DateTime.Now, Description));
        }

        [Fact]
        public void TestExceptionAllNull() {

            Assert.Throws<ArgumentException>(() => new Todo(null, false,
                        null, null));
        }

        [Fact]
        public void TestUpdateDescHandlerException() {
            var handlerUpdate = new UpdateDescTodoHandler(new ControletodosContext());
            UpdateDescTodoRequest teste = new() {
                Id = 0,
                Description = ""
            };

            CancellationToken cancelation = new();
            Assert.ThrowsAsync<ArgumentException>(() => handlerUpdate.Handle(teste, cancelation));
        }

        [Fact]
        public void TestDeleteHandlerException() {
            var handlerUpdate = new DeleteTodoHandler(new ControletodosContext());
            DeleteTodoRequest teste = new() {
                Id = 0
            };

            CancellationToken cancelation = new();
            Assert.ThrowsAsync<ArgumentException>(() => handlerUpdate.Handle(teste, cancelation));
        }

        [Fact]
        public void TestCreateHandlerException() {
            var handlerUpdate = new CreateTodoHandler(new ControletodosContext());
            CreateTodoRequest teste = new() {
                Title = "",
                Complete = false,
                Description = ""
            };

            CancellationToken cancelation = new();
            Assert.ThrowsAsync<ArgumentException>(() => handlerUpdate.Handle(teste, cancelation));
        }

        [Fact]
        public void TestUpdateHandlerException() {
            var handlerUpdate = new UpdateTodoHandler(new ControletodosContext());
            UpdateTodoRequest teste = new() {
                Id = 0,
                Complete = false
            };

            CancellationToken cancelation = new();
            Assert.ThrowsAsync<ArgumentException>(() => handlerUpdate.Handle(teste, cancelation));
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

            CreateTodoRequest teste = new() {
                Title = CreateEsperado.Title,
                Description = CreateEsperado.Description,
                Complete = CreateEsperado.Complete
            };

            CreateEsperado.ToExpectedObject().ShouldMatch(teste);

        }

        [Fact]
        public void TestUpdateRequest() {


            var UpdateEsperado = new {
                Id = 1,
                Complete = false
            };

            UpdateTodoRequest teste = new() {
                Id = 1,
                Complete = false
            };

            UpdateEsperado.ToExpectedObject().ShouldMatch(teste);

        }

        [Fact]
        public void TestUpdateDescRequest() {


            var UpdateDescEsperado = new {
                Id = 1,
                Description = "teste"
            };

            UpdateDescTodoRequest teste = new() {
                Id = 1,
                Description = "teste"
            };

            UpdateDescEsperado.ToExpectedObject().ShouldMatch(teste);

        }

        [Fact]
        public void TestDeleteRequest() {


            var UpdateDeleteEsperado = new {
                Id = 1
            };

            DeleteTodoRequest teste = new() {
                Id = 1
            };

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

            Todo teste = new(Todo.Title, Todo.Complete, Todo.DateComplete, Todo.Description);

            Todo.ToExpectedObject().ShouldMatch(teste);

        }

    }
}
