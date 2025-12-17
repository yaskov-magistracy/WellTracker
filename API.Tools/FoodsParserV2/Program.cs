using FoodsParserV2;

var parser = new OpenFoodFactsParser();
// TODO: Add PageSkipping
var products = await parser.GetRussianProductsAsync(150, 15);
await FoodsCsvWriter.SaveToCsvAsync(products);

var a = 10;