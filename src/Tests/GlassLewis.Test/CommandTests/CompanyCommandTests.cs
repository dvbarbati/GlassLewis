using GlassLewis.Company.API.Application.Commands;
using System.Linq;
using Xunit;

namespace GlassLewis.Test.CommandTests
{
    public class CompanyCommandTests
    {
        [Fact]
        public void Create_company_command_with_valid_should_process_and_return_valid_response()
        {
            // Arrange
            var command = new CreateCompanyCommand("Apple Inc.", "AAPL", "NASDAQ", "US0378331005", "http://www.apple.com");

            // Act
            var result = command.IsValid();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Create_company_command_with_invalid_command_should_fail_and_return_validations_messages()
        {
            // Arrange
            var command = new CreateCompanyCommand("", "", "", "", "");

            // Act Assert
            Assert.False(command.IsValid());
            Assert.Equal(4, command.ValidationResult.Errors.Count);
            Assert.Contains(CreateCompanyCommandValidation.NameErrorMessage, command.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CreateCompanyCommandValidation.ExchangeErrorMessage, command.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CreateCompanyCommandValidation.ISINErrorMessage, command.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CreateCompanyCommandValidation.StockTickerErrorMessage, command.ValidationResult.Errors.Select(c => c.ErrorMessage));

        }

        [Fact]
        
        public void Create_company_command_with_isin_containing_more_characters_than_allowed_should_fail_and_return_validation_message()
        {
            // Arrange
            var command = new CreateCompanyCommand("Apple Inc.", "AAPL", "NASDAQ", "US037833100500", "http://www.apple.com");

            // Act Assert
            Assert.False(command.IsValid());
            Assert.Equal(1, command.ValidationResult.Errors.Count);
            Assert.Contains(CreateCompanyCommandValidation.ISINErrorMessage, command.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact]
        public void Create_company_command_with_isin_containing_fewer_characters_than_allowed_should_fail_and_return_validation_message()
        {
            // Arrange
            var command = new CreateCompanyCommand("Apple Inc.", "AAPL", "NASDAQ", "US03780500", "http://www.apple.com");

            // Act and Assert
            Assert.False(command.IsValid());
            Assert.Equal(1, command.ValidationResult.Errors.Count);
            Assert.Contains(CreateCompanyCommandValidation.ISINErrorMessage, command.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact]
        public void Create_company_command_with_isin_starting_with_numbers_should_fail_and_return_validation_message()
        {
            // Arrange
            var command = new CreateCompanyCommand("Apple Inc.", "AAPL", "NASDAQ", "120378050015", "http://www.apple.com");

            // Act and Assert
            Assert.False(command.IsValid());
            Assert.Equal(1, command.ValidationResult.Errors.Count);
            Assert.Contains(CreateCompanyCommandValidation.ISINErrorMessage, command.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
