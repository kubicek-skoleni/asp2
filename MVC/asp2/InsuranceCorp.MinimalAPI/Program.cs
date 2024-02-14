using InsuranceCorp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InsCorpDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/", () => "hello");

app.MapGet("/person/{id:int}", (int id, InsCorpDbContext db) =>
	db.Persons.Find(id)
);

app.MapGet("/person/list", (int start, int take, InsCorpDbContext db) =>
	db.Persons.Skip(start).Take(take)
); 

app.Run();


