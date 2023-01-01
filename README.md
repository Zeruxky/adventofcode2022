<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<div>
<h3 align="center">AdventOfCode 2022</h3>

  <p align="center">
    Contains all solutions for the <a href="https://adventofcode.com/2022">Advent of Code (AoC) 2022</a>. You can solve the puzzles of the AoC 2022 by compiling the program to a CLI tool.
    <br />
    <a href="https://github.com/Zeruxky/adventofcode2022"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Zeruxky/adventofcode2022">View Demo</a>
    ·
    <a href="https://github.com/Zeruxky/adventofcode2022/issues">Report Bug</a>
    ·
    <a href="https://github.com/Zeruxky/adventofcode2022/issues">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
    <li><a href="#dependencies">Dependencies</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

Solvers for the puzzles of [Advent of Code 2022 (AoC 2022)](https://adventofcode.com/) implemented by myself. It provides a CLI tool with following functionalities:

- Selection of a specific day and part from the AoC 2022
- Solving of puzzles from the AoC 2022
- Downloads automatically the input for the selected day and part.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

* [![Dotnet][Dotnet]][Next-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/Zeruxky/adventofcode2022.git
   ```
2. Build
   ```sh
   dotnet build
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->
## Usage

First create a file with the name "adventofcode_settings.yml" on the same level as your compiled exe. In this file you set following settings

```yml
base_address: <the_base_adress_of_the_AoC> (Optional)
session_token: <your_session_token_for_AoC>
```

A example setting would be:

```yml
base_address: https://adventofcode.com/2022/
session_token: session=abc...xyz
```

To use the CLI tool, execute following command within the project directory:
```sh
dotnet run
```

Navigate with your key arrows up and down to select a day and comfirm it with pressing enter. Next you can select, if you want to solve both parts of the selected day, just part one or part two.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->
## Roadmap

- [x] Day 1
- [x] Day 2
- [x] Day 3
- [x] Day 4
- [x] Day 5
- [x] Day 6
- [x] Day 7
- [x] Day 8
- [ ] Day 9
- [ ] Day 10
- [ ] Day 11
- [ ] Day 12
- [ ] Day 13
- [ ] Day 14
- [ ] Day 15
- [ ] Day 16
- [ ] Day 17
- [ ] Day 18
- [ ] Day 19
- [ ] Day 20
- [ ] Day 21
- [ ] Day 22
- [ ] Day 23
- [ ] Day 24
- [ ] Day 25

See the [open issues](https://github.com/Zeruxky/adventofcode2022/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Your Name - [@philipwille](https://twitter.com/philipwille) - philip@philipwille.com.com

Project Link: [https://github.com/Zeruxky/adventofcode2022](https://github.com/Zeruxky/adventofcode2022)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Eric Wastl for creating AoC](https://was.tl/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Dependencies

* [Spectre.Console](https://spectreconsole.net/)
* [Ardalis SmartEnum](https://github.com/ardalis/SmartEnum)
* [System.Linq.Async](https://github.com/dotnet/reactive)
* [YamlDotNet](https://github.com/aaubry/YamlDotNet)


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Zeruxky/adventofcode2022.svg?style=for-the-badge
[contributors-url]: https://github.com/Zeruxky/adventofcode2022/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Zeruxky/adventofcode2022.svg?style=for-the-badge
[forks-url]: https://github.com/Zeruxky/adventofcode2022/network/members
[stars-shield]: https://img.shields.io/github/stars/Zeruxky/adventofcode2022.svg?style=for-the-badge
[stars-url]: https://github.com/Zeruxky/adventofcode2022/stargazers
[issues-shield]: https://img.shields.io/github/issues/Zeruxky/adventofcode2022.svg?style=for-the-badge
[issues-url]: https://github.com/Zeruxky/adventofcode2022/issues
[license-shield]: https://img.shields.io/github/license/Zeruxky/adventofcode2022.svg?style=for-the-badge
[license-url]: https://github.com/Zeruxky/adventofcode2022/blob/master/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/philip-wille
[product-screenshot]: images/screenshot.png
[Dotnet]: https://img.shields.io/badge/dotnet-000000?style=for-the-badge&logo=csharp&logoColor=white
[Next-url]: https://dotnet.microsoft.com/en-us/
