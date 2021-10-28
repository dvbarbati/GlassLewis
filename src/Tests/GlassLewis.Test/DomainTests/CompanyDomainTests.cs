using GlassLewis.Company.Domain.Entities;
using GlassLewis.Core.DomainObjetcs;
using System.Linq;
using Xunit;

namespace GlassLewis.Test.DomainTests
{
    
    public class CompanyDomainTests
    {
        [Fact]
        public void Create_company_entity_with_isin_containing_fewer_characters_than_allowed_should_fail_and_return_validation_message()
        {
            var company = new Company.Domain.Entities.Company("Apple Inc.", "AAPL", "NASDAQ", "078331012", "http://www.apple.com");

            var result = company.IsValid();

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Create_company_entity_with_isin_containing_more_characters_than_allowed_should_fail_and_return_validation_message()
        {
            var company = new Company.Domain.Entities.Company("Apple Inc.", "AAPL", "NASDAQ", "as07021jjhjk8331012", "http://www.apple.com");

            var result = company.IsValid();

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Create_company_entity_with_invalid_values_should_fail_and_return_validation_messages()
        {
            var company = new Company.Domain.Entities.Company("", "", "", "", "");

            var result = company.IsValid();

            Assert.False(result.IsValid);
            Assert.Equal(4, result.Errors.Count);
            Assert.Contains(CompanyValidation.NameErrorMessage, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CompanyValidation.ExchangeErrorMessage, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CompanyValidation.ISINErrorMessage, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CompanyValidation.StockTickerErrorMessage, result.Errors.Select(c => c.ErrorMessage));

        }

        [Fact]
        public void Create_company_entity_with_isin_starting_with_numbers_should_fail_and_return_validation_message()
        {
           var company = new Company.Domain.Entities.Company("Apple Inc.", "AAPL", "NASDAQ", "120378331012", "http://www.apple.com");

            var result = company.IsValid();

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(CompanyValidation.ISINErrorMessage, result.Errors.Select(c => c.ErrorMessage));
        }

        [Fact]
        public void Create_company_entity_with_valid_informations_should_be_valid_and_without_validation_message()
        {
           var company = new Company.Domain.Entities.Company("Apple Inc.", "AAPL", "NASDAQ", "US0378331012", "http://www.apple.com");

            Assert.Equal( "Apple Inc.", company.Name);
            Assert.Equal("AAPL", company.StockTicker);
            Assert.Equal("NASDAQ", company.Exchange );
            Assert.Equal( "US0378331012", company.ISIN);
            Assert.Equal( "http://www.apple.com", company.Website);

            Assert.True(company.IsValid().IsValid);
            Assert.Equal(0, company.Notifications.Count);
        }

    }
}
