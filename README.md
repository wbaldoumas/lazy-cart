# lazy-cart 🚀

**Project Status**: Discovery. The project is still in its early stages. To report a bug, feel free to open an [issue](https://github.com/wbaldoumas/lazy-cart/issues).

[![Build][github-checks-shield]][github-checks-url]
[![Coverage][coverage-shield]][coverage-url]

[![Version][nuget-version-shield]][nuget-url]
[![Downloads][nuget-downloads-shield]][nuget-url]

[![Contributor Covenant][contributor-covenant-shield]][contributor-covenant-url]
[![Contributors][contributors-shield]][contributors-url]
[![Commits][last-commit-shield]][last-commit-url]

[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

[![LinkedIn][linkedin-shield]][linkedin-url]

## 🎯 About The Project

LazyCart is a lightweight .NET library designed to lazily generate the Nth entry of the Cartesian product of multiple sets. Cartesian products can grow exponentially in size with the addition of each new set, making it untenable to generate the entire product in memory for larger or more numerous sets.

LazyCart solves this problem by providing a way to generate individual entries of the product on-demand. This allows you to work with very large Cartesian products without the need for enormous amounts of memory. It's also perfect for scenarios where you don't need the full Cartesian product but want to retrieve specific entries or generate a random sample.

LazyCart enables you to find the index of a specific Cartesian product entry, a feature that can be useful when dealing with ordered data sets.

These features make LazyCart a powerful tool for any application that requires efficient and flexible handling of large data sets. This includes a wide range of fields, from data analysis and machine learning to gaming and simulations.

### ✨ Features

- **Efficient Computation**: LazyCart enables efficient calculation of the Nth entry of the Cartesian product of N sets, allowing for real-time usage without the need to pre-calculate all combinations.
- **Massive Scale**: Handle Cartesian products of colossal sets or multiple large sets without worrying about memory constraints. This library was built to accommodate use cases with millions or even billions of combinations.
- **Random Sampling**: Easily generate evenly-distributed, random samples of your Cartesian product entries. This is great for exploratory data analysis or simulations.
- **Find Index of Entries**: This library allows you to find the index of a specific entry in the Cartesian product, useful for a variety of mathematical and computational applications.
- **Generic and Type-Safe**: LazyCart is implemented in a generic way, allowing it to work with data of any type. This makes it versatile and ensures type safety in your applications.
- **Simple API**: The library provides an easy-to-use API, making it straightforward to use in your projects. Its usage is clear and intuitive, and it fits well within the .NET ecosystem.

## 🤖 Installation

### 📦 Package Manager

LazyCart is offered as a [NuGet package](https://www.nuget.org/packages/LazyCart) and can be installed with the following command, or your favorite package manager:

```shell
dotnet add package LazyCart --version 0.4.1
```

### 🛠️ Building From Source

To build from source, clone the repository locally and run some flavor of the following command:

```shell
git clone https://github.com/wbaldoumas/lazy-cart.git
cd lazy-cart
dotnet build src --configuration Release
```

## 🌌 Usage

Let's say we're designing a character system for a new online game. We have a number of attributes for our characters, including race, class, weapon, armor type, and skills, with each attribute having many possibilities. We can use LazyCart to generate these combinations.

```csharp
var races = new List<string> {"Human", "Elf", "Dwarf", "Orc", "Goblin", "Troll", "Gnome"};
var classes = new List<string> {"Warrior", "Mage", "Rogue", "Paladin", "Hunter", "Druid", "Warlock", "Monk"};
var weapons = new List<string> {"Sword", "Staff", "Bow", "Dagger", "Mace", "Axe", "Polearm", "Wand", "Fist Weapon"};
var armors = new List<string> {"Plate", "Mail", "Leather", "Cloth"};
var skills = new List<string> {"Fire", "Ice", "Stealth", "Heal", "Shadow", "Light", "Earth", "Wind", "Water", "Arcane"};

var lazyCart = new LazyCartesianProduct<string, string, string, string, string>(races, classes, weapons, armors, skills);
```

Now, we can easily get the 20000th possible character configuration:

```csharp
// get the 20000th configuration of ("Gnome", "Monk", "Axe", "Plate", "Fire")
var character = lazyCart[20000];
```

If we want to find the index of a specific character configuration, we can do that as well:

```csharp
// get the index of this character configuration
var index = lazyCart.IndexOf(("Elf", "Mage", "Staff", "Cloth", "Fire"));
```

And if we need to generate a random sample of 10 possible character configurations:

```csharp
// get an IEnumerable of 10 random character configurations
var sample = lazyCart.GenerateSamples(10);
```

## 🗺️ Roadmap

See the [open issues](https://github.com/wbaldoumas/lazy-cart/issues) for a list of proposed features (and known issues).

## 🤝 Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**. For detailed contributing guidelines, please see the [CONTRIBUTING](https://github.com/wbaldoumas/lazy-cart/blob/main/CONTRIBUTING.md) docs.

## 📜 License

Distributed under the `MIT License` License. See [`LICENSE`](https://github.com/wbaldoumas/lazy-cart/blob/main/LICENSE) for more information.

## Contact

[@wbaldoumas](https://github.com/wbaldoumas)

Project Link: [https://github.com/wbaldoumas/lazy-cart](https://github.com/wbaldoumas/lazy-cart)

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/wbaldoumas/lazy-cart.svg?style=for-the-badge
[contributors-url]: https://github.com/wbaldoumas/lazy-cart/graphs/contributors
[contributor-covenant-shield]: https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg?style=for-the-badge
[contributor-covenant-url]: https://github.com/wbaldoumas/lazy-cart/blob/main/CODE_OF_CONDUCT.md
[forks-shield]: https://img.shields.io/github/forks/wbaldoumas/lazy-cart.svg?style=for-the-badge
[forks-url]: https://github.com/wbaldoumas/lazy-cart/network/members
[stars-shield]: https://img.shields.io/github/stars/wbaldoumas/lazy-cart.svg?style=for-the-badge
[stars-url]: https://github.com/wbaldoumas/lazy-cart/stargazers
[issues-shield]: https://img.shields.io/github/issues/wbaldoumas/lazy-cart.svg?style=for-the-badge
[issues-url]: https://github.com/wbaldoumas/lazy-cart/issues
[license-shield]: https://img.shields.io/github/license/wbaldoumas/lazy-cart.svg?style=for-the-badge
[license-url]: https://github.com/wbaldoumas/lazy-cart/blob/main/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/williambaldoumas
[coverage-shield]: https://img.shields.io/codecov/c/github/wbaldoumas/lazy-cart?style=for-the-badge
[coverage-url]: https://app.codecov.io/gh/wbaldoumas/lazy-cart/branch/main
[last-commit-shield]: https://img.shields.io/github/last-commit/wbaldoumas/lazy-cart?style=for-the-badge
[last-commit-url]: https://github.com/wbaldoumas/lazy-cart/commits/main
[github-checks-shield]: https://img.shields.io/github/actions/workflow/status/wbaldoumas/lazy-cart/test.yml?style=for-the-badge
[github-checks-url]: https://github.com/wbaldoumas/lazy-cart/actions
[nuget-version-shield]: https://img.shields.io/nuget/v/LazyCart?style=for-the-badge
[nuget-downloads-shield]: https://img.shields.io/nuget/dt/LazyCart?style=for-the-badge
[nuget-url]: https://www.nuget.org/packages/LazyCart/
