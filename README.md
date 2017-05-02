# Cake.DotEnv.Module
Cake Module for using the dotenv file for settings local environment variables.

The concept is that you would create a **.env** file in the root of your project. You should make sure to put **.env** in your gitignore file. Then you will set any environment variables you need locally in your Cake script in that file.  The module would load those environment variables so you can then inject those into your build script.  If the file is not present then it is ignored.

This way your do not clog up your local system with environment variables.
