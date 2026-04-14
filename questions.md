# Vaihe 3: Service-kerros, Repository, Result Pattern ja API-dokumentaatio — Teoriakysymykset

Vastaa alla oleviin kysymyksiin omin sanoin. Kirjoita vastauksesi kysymysten alle.

> **Vinkki:** Jos jokin kysymys tuntuu vaikealta, palaa lukemaan teoriamateriaalit:
>
> - [Service-kerros ja DI](https://github.com/xamk-mire/Xamk-wiki/blob/main/C%23/fin/04-Advanced/WebAPI/Services-and-DI.md)
> - [Repository Pattern](https://github.com/xamk-mire/Xamk-wiki/blob/main/C%23/fin/04-Advanced/Patterns/Repository-Pattern.md)
> - [Result Pattern](https://github.com/xamk-mire/Xamk-wiki/blob/main/C%23/fin/04-Advanced/Patterns/Result-Pattern.md)

---

## Osa 1: Service-kerros

### Kysymys 1: Fat Controller -ongelma

Miksi on ongelma jos controller sisältää kaiken logiikan (tietokantakyselyt, muunnokset, validoinnin)? Anna vähintään kaksi konkreettista haittaa.

**Vastaus: Testaaminen vaikeaa, pitäisi käyttää oikeaa tietokantaa.
Contoller kasvaa älyttömän suureksi, logiikka on kevyempää luettavaa kun koodi on jaoteltu services kansioon.**

---

### Kysymys 2: Vastuunjako

Miten vastuut jakautuvat controller:n, service:n ja repository:n välillä tässä harjoituksessa? Kirjoita lyhyt kuvaus kunkin kerroksen tehtävästä.

**Controller vastaa: Pelkistä http pyynnöistä, get, post, update, delete. Contoller on hyvä pitää langan ohuena, se vain vastaamottaa pyynnöt, kutsuu serviseä, palauttaa vastauksem.**

**Service vastaa: Logiikasta, mitä sovellus tekee.**

**Repository vastaa: Miten data haetaan.**

---

### Kysymys 3: DTO-muunnokset servicessä

Miksi DTO ↔ Entity -muunnokset kuuluvat serviceen eikä controlleriin? Mitä hyötyä siitä on, että controller ei tunne `Product`-entiteettiä lainkaan?

**Vastaus:**

---

## Osa 2: Interface ja Dependency Injection

### Kysymys 4: Interface vs. konkreettinen luokka

Miksi controller injektoi `IProductService`-interfacen eikä suoraan `ProductService`-luokkaa? Mitä hyötyä tästä on?

**Vastaus: siksi koska muuten mahdoton testata, tarvitaan sitä interfacea.**

---

### Kysymys 5: DI-elinkaaret

Selitä ero näiden kolmen elinkaaren välillä ja anna esimerkki milloin kutakin käytetään:

- **AddScoped: yhden http pyynnön mittainen elinkaari.**
- **AddSingleton: Sovelluksen elinkaaren mittainen, käytetään välimuistina.**
- **AddTransient: Luodaan uusi jokakerta.**

Miksi `AddScoped` on oikea valinta `ProductService`:lle?

## **Vastaus: Koska käytetään DbContextia, joka itsessään on scoped, käytetään myös http pyynnössä svoped instanssia**

### Kysymys 6: DI-kontti

Selitä omin sanoin mitä DI-kontti tekee kun HTTP-pyyntö saapuu ja `ProductsController` tarvitsee `IProductService`:ä. Mitä tapahtuu vaihe vaiheelta?

**Vastaus:**

---

### Kysymys 7: Rekisteröinnin unohtaminen

Mitä tapahtuu jos unohdat rekisteröidä `IProductService`:n `Program.cs`:ssä? Milloin virhe ilmenee ja miltä se näyttää?

**Vastaus: Di kontti ei osaa reitittää.**

---

## Osa 3: Repository-kerros

### Kysymys 8: Miksi repository?

`ProductService` käytti aluksi `AppDbContext`:ia suoraan. Miksi se refaktoroitiin käyttämään `IProductRepository`:a? Anna vähintään kaksi syytä.

**Vastaus: 1 : Testien kannalta, ei voida testata ilman oikeaa tietokantaa.
2 : **

---

### Kysymys 9: Service vs. Repository

Mikä on `IProductService`:n ja `IProductRepository`:n välinen ero? Mitä tietotyyppejä kumpikin käsittelee (DTO vai Entity)?

**IProductService: Käsittelee pyyntöä **

**IProductRepository:**

---

### Kysymys 10: Controllerin muuttumattomuus

Kun Vaihe 7:ssä lisättiin repository-kerros, `ProductsController` ei muuttunut lainkaan. Miksi? Mitä tämä kertoo rajapintojen (interface) hyödystä?

**Vastaus:**

---

## Osa 4: Exception-käsittely ja lokitus

### Kysymys 11: ILogger

Mikä on `ILogger` ja miksi sitä tarvitaan? Mistä lokit näkee kehitysympäristössä?

**Vastaus:**

---

### Kysymys 12: Odotetut vs. odottamattomat virheet

Selitä ero "odotetun" ja "odottamattoman" virheen välillä. Anna esimerkki kummastakin ja kerro miten ne käsitellään eri tavalla servicessä.

**Odotettu virhe (esimerkki + käsittely):**

**Odottamaton virhe (esimerkki + käsittely):**

---

## Osa 5: Result Pattern

### Kysymys 13: Miksi null ja bool eivät riitä?

Alla on kaksi esimerkkiä. Selitä miksi ensimmäinen tapa on ongelmallinen ja miten toinen ratkaisee ongelman:

```csharp
// Tapa 1: null
ProductResponse? product = await _service.GetByIdAsync(id);
if (product == null)
    return NotFound();

// Tapa 2: Result
Result<ProductResponse> result = await _service.GetByIdAsync(id);
if (result.IsFailure)
    return NotFound(new { error = result.Error });
```

**Vastaus:**

---

### Kysymys 14: Result.Success vs. Result.Failure

Miten `Result Pattern` muutti virheiden käsittelyä servicessä? Vertaa Vaihe 8:n `throw;`-tapaa Vaihe 9:n `Result.Failure`-tapaan: mitä eroa niillä on asiakkaan (API:n kutsuja) näkökulmasta?

**Vastaus:**

---

## Osa 6: API-dokumentaatio

### Kysymys 15: IActionResult vs. ActionResult\<T\>

Miksi `ActionResult<ProductResponse>` on parempi kuin `IActionResult`? Anna vähintään kaksi syytä.

**Vastaus:**

---

### Kysymys 16: ProducesResponseType

Mitä `[ProducesResponseType]`-attribuutti tekee? Miten se näkyy Swagger UI:ssa?

**Vastaus:**

---

### Kysymys 18: Refaktorointi

Sovelluksen toiminnallisuus pysyi täysin samana koko harjoituksen ajan — samat endpointit, samat vastaukset. Mitä refaktorointi tarkoittaa ja miksi se kannattaa, vaikka käyttäjä ei huomaa eroa?

**Vastaus:**

---
