﻿using FluentValidation;
using FluentValidation.Results;
using GlassLewis.Core.DomainObjetcs;
using GlassLewis.Core.DomainObjetcs.Interfaces;
using System;
using System.Linq;

namespace GlassLewis.Company.Domain.Entities
{
    public class Company : Entity, IAggregateRoot
    {
       
        public string Name { get; private set; }
        public string StockTicker { get; private set; }
        public string Exchange { get; private set; }
        public string ISIN { get; private set; }
        public string Website { get; private set; }

        protected Company() { }

        public Company( string name, string stockTicker, string exchange, string isin, string website)
        {
            Name = name;
            StockTicker = stockTicker;
            Exchange = exchange;
            ISIN = isin;
            Website = website;
        }

        public Company(Guid id, string name, string stockTicker, string exchange, string isin, string website)
        {
            Id = id;
            Name = name;
            StockTicker = stockTicker;
            Exchange = exchange;
            ISIN = isin;
            Website = website;
        }

        public ValidationResult IsValid()
        {
            return new CompanyValidation().Validate(this);
        }
    }

    public class CompanyValidation : AbstractValidator<Company>
    {
        public static string NameErrorMessage => "The name is empty.";
        public static string ExchangeErrorMessage => "The exchange is empty.";
        public static string StockTickerErrorMessage => "The stock ticker is empty.";
        public static string ISINErrorMessage => "the ISIN must be 12 characters long and must have letters in the first two digits.";

        public CompanyValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(NameErrorMessage);

            RuleFor(c => c.Exchange)
                .NotEmpty()
                .WithMessage(ExchangeErrorMessage);

            RuleFor(c => c.StockTicker)
                .NotEmpty()
                .WithMessage(StockTickerErrorMessage);

            RuleFor(c => c.ISIN)
               .Must(ValidateISIN)
               .WithMessage(ISINErrorMessage);

        }

        private bool ValidateISIN(string isin)
        {
            if (string.IsNullOrEmpty(isin))
                return false;

            if (isin.Length != 12)
                return false;

            var countryCode = isin.Substring(0, 2);

            var countLetters = countryCode.Where(x => char.IsLetter(x)).Count();

            if (countLetters != 2)
                return false;

            return true;

        }
    }
}
