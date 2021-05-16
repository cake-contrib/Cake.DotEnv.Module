# Cake.DotEnv.Module

Cake Module for using the dotenv file for settings local environment variables.

The concept is that you would create a **.env** file in the root of your project. You should make sure to put **.env** in your gitignore file. Then you will set any environment variables you need locally in your Cake script in that file.  The module would load those environment variables so you can then inject those into your build script.  If the file is not present then it is ignored.

This way your do not clog up your local system with environment variables.

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Release History

Click on the [Releases](https://github.com/cake-contrib/Cake.DotEnv.Module/releases) tab on GitHub.

---

_Copyright &copy; 2017-2021 Cake Contributors - Provided under the [MIT License](LICENSE)._
