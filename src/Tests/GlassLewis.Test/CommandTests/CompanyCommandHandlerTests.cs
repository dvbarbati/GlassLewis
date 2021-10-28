using GlassLewis.Company.API.Application.Commands;
using GlassLewis.Company.API.Application.Handller;
using GlassLewis.Company.Domain.Interfaces.Repository;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GlassLewis.Test.CommandTests
{
    public class CompanyCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly CompanyCommandHandler _commandHandler;

        public CompanyCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _commandHandler = _mocker.CreateInstance<CompanyCommandHandler>();
        }

        #region Create Company

        [Fact]
        public async Task Create_company_with_valid_command_and_non_existent_isin_should_process_successfully()
        {
            // Arrange
            var command = new CreateCompanyCommand("Apple Inc.", "AAPL", "NASDAQ", "US0378331005", "http://www.apple.com");

            _mocker.GetMock<ICompanyRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<ICompanyRepository>().Verify(r => r.Add(It.IsAny<Company.Domain.Entities.Company>()), Times.Once);
            _mocker.GetMock<ICompanyRepository>().Verify(r => r.GetByISIN(command.ISIN), Times.Once);
            _mocker.GetMock<ICompanyRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);

        }

        [Fact]
        public async Task Create_company_with_valid_command_and_isin_existing_should_fail_and_return_isin_validation_message()
        {
            // Arrange
            var command = new CreateCompanyCommand("Apple Inc.", "AAPL", "NASDAQ", "US0378331005", "http://www.apple.com");
            var company = new GlassLewis.Company.Domain.Entities.Company("Apple Inc.", "AAPL", "NASDAQ", "US0378331005", "http://www.apple.com");

            _mocker.GetMock<ICompanyRepository>().Setup(r => r.GetByISIN(command.ISIN)).Returns(Task.FromResult(company));
            _mocker.GetMock<ICompanyRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("ISIN is already registered", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task Create_company_with_invalid_command_should_fail_and_return_invalid_result()
        {
            // Arrange
            var command = new CreateCompanyCommand("", "", "", "", "");

            _mocker.GetMock<ICompanyRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            _mocker.GetMock<ICompanyRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);

        }

        #endregion

        #region Edit Company

        [Fact]
        public async Task Edit_company_with_valid_command_should_run_and_return_valid_result()
        {
            // Arrange
            var CompanyId = Guid.NewGuid();
            var command = new UpdateCompanyCommand(CompanyId, "Apple Inc.", "AAPL", "NASDAQ", "US0378331005", "http://www.apple.com");
            var company = new GlassLewis.Company.Domain.Entities.Company(CompanyId, "Apple Inc.", "AAPL", "NASDAQ", "US0378331005", "http://www.apple.com");

            _mocker.GetMock<ICompanyRepository>().Setup(r => r.GetByISIN(command.ISIN)).Returns(Task.FromResult(company));
            _mocker.GetMock<ICompanyRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<ICompanyRepository>().Verify(r => r.GetByISIN(command.ISIN), Times.Once);
            _mocker.GetMock<ICompanyRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task Edit_company_with_invalid_command_should_return_invalid_result()
        {
            // Arrange
            var command = new UpdateCompanyCommand(Guid.Empty, "", "", "", "", "");

            _mocker.GetMock<ICompanyRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        #endregion
    }
}
