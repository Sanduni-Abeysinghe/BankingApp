using Moq;
using BankingSystemAPI.Controllers;
using BankingSystemAPI.Services.Interfaces;
using BankingSystemAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystemAPI.Tests.Controllers
{
    public class TransactionsControllerTests
    {
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<ILogger<TransactionsController>> _loggerMock;
        private readonly TransactionsController _controller;

        public TransactionsControllerTests()
        {
            _transactionServiceMock = new Mock<ITransactionService>();
            _loggerMock = new Mock<ILogger<TransactionsController>>();
            _controller = new TransactionsController(_transactionServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTransactions_ShouldReturnOk_WhenTransactionsExist()
        {
            var transactionList = new List<TransactionDTO>
            {
new TransactionDTO { Id = 1, AccountId = 1, Amount = 100, TransactionDate = DateTime.Now },
                new TransactionDTO { Id = 2, AccountId = 2, Amount = 200, TransactionDate = DateTime.Now }
            };
            _transactionServiceMock.Setup(service => service.GetAllTransactionsAsync()).ReturnsAsync(transactionList);

            var result = await _controller.GetTransactions();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TransactionDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetTransactions_ShouldReturnNotFound_WhenNoTransactionsExist()
        {
            _transactionServiceMock.Setup(service => service.GetAllTransactionsAsync()).ReturnsAsync(new List<TransactionDTO>());

            var result = await _controller.GetTransactions();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TransactionDTO>>(okResult.Value);
            Assert.Empty(returnValue); // No transactions should be returned
        }

        [Fact]
        public async Task GetTransaction_ShouldReturnOk_WhenTransactionExists()
        {
            var transaction = new TransactionDTO { Id = 1, AccountId = 1, Amount = 100, TransactionDate = DateTime.Now };
            _transactionServiceMock.Setup(service => service.GetTransactionByIdAsync(1)).ReturnsAsync(transaction);

            var result = await _controller.GetTransaction(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TransactionDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetTransaction_ShouldReturnNotFound_WhenTransactionDoesNotExist()
        {
            _transactionServiceMock.Setup(service => service.GetTransactionByIdAsync(1)).ReturnsAsync((TransactionDTO)null);

            var result = await _controller.GetTransaction(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task CreateTransaction_ShouldReturnCreatedAtAction_WhenTransactionIsCreatedSuccessfully()
        {
            var createTransactionDTO = new CreateTransactionDTO {AccountId = 1, Amount = 100, TransactionDate = DateTime.Now };
            _transactionServiceMock.Setup(service => service.CreateTransactionAsync(createTransactionDTO)).ReturnsAsync(true);

            var result = await _controller.CreateTransaction(createTransactionDTO);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTransaction", createdAtActionResult.ActionName);
            Assert.Equal(createTransactionDTO.AccountId, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task CreateTransaction_ShouldReturnBadRequest_WhenTransactionCreationFails()
        {
            var createTransactionDTO = new CreateTransactionDTO { AccountId = 1, Amount = 100, TransactionDate = DateTime.Now };
            _transactionServiceMock.Setup(service => service.CreateTransactionAsync(createTransactionDTO)).ReturnsAsync(false);

            var result = await _controller.CreateTransaction(createTransactionDTO);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
