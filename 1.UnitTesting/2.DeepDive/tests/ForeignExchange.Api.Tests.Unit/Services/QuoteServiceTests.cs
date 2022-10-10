using Bogus;
using FluentAssertions;
using ForeignExchange.Api.Logging;
using ForeignExchange.Api.Models;
using ForeignExchange.Api.Repositories;
using ForeignExchange.Api.Services;
using ForeignExchange.Api.Validation;
using NSubstitute;
using Xunit;

namespace ForeignExchange.Api.Tests.Unit.Services;

public class QuoteServiceTests
{
    private readonly QuoteService _sut;
    private readonly IRatesRepository _ratesRepository = Substitute.For<IRatesRepository>();
    private readonly ILoggerAdapter<QuoteService> _logger = Substitute.For<ILoggerAdapter<QuoteService>>();

    private readonly Faker<FxRate> _faker = new Faker<FxRate>()
        .RuleFor(x => x.FromCurrency, faker => faker.Finance.Currency().Code)
        .RuleFor(x => x.ToCurrency, faker => faker.Finance.Currency().Code)
        .RuleFor(x => x.Rate, faker => faker.Finance.Random.Decimal(1, 2));

    public QuoteServiceTests()
    {
        _sut = new QuoteService(
            _ratesRepository,
            _logger);
    }

    [Fact]
    public async Task GetQuoteAsync_ReturnsQuote_WhenCurrenciesAreSupported()
    {
        // Arrange
        var fromCurrency = "GBP";
        var toCurrency = "AUD";
        var amount = 100;
        
        _ratesRepository.GetRateAsync(fromCurrency, toCurrency)
            .Returns(new FxRate
            {
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Rate = 1.6m
            });

        var expectedQuote = new ConversionQuote
        {
            BaseCurrency = fromCurrency,
            QuoteCurrency = toCurrency,
            BaseAmount = amount,
            QuoteAmount = 160
        };

        // Act
        var quote = await _sut.GetQuoteAsync(fromCurrency, toCurrency, amount);

        // Assert
        quote!.Should().BeEquivalentTo(expectedQuote);
    }

    [Fact]
    public async Task GetQuoteAsync_ShouldThrowNegativeAmountException_WhenAmountIsNegative()
    {
        // Arrange
        var fromCurrency = "GBP";
        var toCurrency = "AUD";
        var amount = -5;
        
        // Act
        var quoteAction = () => _sut.GetQuoteAsync(fromCurrency, toCurrency, amount);

        // Assert
        await quoteAction.Should().ThrowAsync<NegativeAmountException>()
            .WithMessage("You can only convert a positive amount of money");
    }

    [Fact]
    public async Task GetQuoteAsync_LogsMessage_WhenInvoked()
    {
        // Arrange
        var fromCurrency = "GBP";
        var toCurrency = "AUD";
        var amount = 100;
        
        _ratesRepository.GetRateAsync(fromCurrency, toCurrency)
            .Returns(new FxRate
            {
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Rate = 1.6m
            });
        
        // Act
        await _sut.GetQuoteAsync(fromCurrency, toCurrency, amount);

        // Assert
        _logger.Received(1).LogInformation("Retrieved quote for currencies {FromCurrency}->{ToCurrency} in {ElapsedMilliseconds}ms", 
            Arg.Is<object[]>(x => x[0].ToString() == fromCurrency && x[1].ToString() == toCurrency));
    }
}
