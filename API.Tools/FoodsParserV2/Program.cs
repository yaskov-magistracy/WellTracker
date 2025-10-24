using FoodsParserV2;

var parser = new OpenFoodFactsParser();
var products = await parser.GetRussianProductsAsync(100, 1);
await FoodsCsvWriter.SaveToCsvAsync(products);

var a = 10;