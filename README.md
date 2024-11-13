**Guia de instalação do programa SNMPWALK-WEB**

> Criar uma pasta no linux para centralizar os arquivos e obter uma melhor organização.
> Adicionar o arquivo "projetopuc.sh", "program.cs", "estilo.css" e "index.html" na pasta
> Realizar a instalação do DotNet seguindo as etapas no final da pagina
> Executar o DotNet utilizando o comando "dotnet run" dentro da pasta criada acima
> Realizar a instalação do Visual Studio Code seguindo as etapas no final da pagina
> Executar o Visual Studio Code na mesma pasta criada anteriormente e então adicionar a extensão "Live Server" através do botão de "Extensões (Ctrl+Shift+X)"
> Executar o codigo "index.html" no botão "Go Live" localizado no canto inferior do Visual Studio Code e então colocar suas informações para rodar o comando, exemplo abaixo:

![image](https://github.com/user-attachments/assets/73f8588e-de72-4b43-bb32-a4fd69b292bb)












**Guia de instalação do DotNet (Ubuntu)**
> sudo apt update
> sudo apt install -y wget apt-transport-https
> wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
> sudo dpkg -i packages-microsoft-prod.deb
> sudo apt update
> sudo apt install -y dotnet-sdk-7.0

**Guia de instalação do DotNet (Debian)**
> sudo apt update
> sudo apt install -y wget apt-transport-https
> wget https://packages.microsoft.com/config/debian/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
> sudo dpkg -i packages-microsoft-prod.deb
> sudo apt update
> sudo apt install -y dotnet-sdk-7.0

**Guia de instalação Visual Studio Code (Ubuntu & Debian)**
> Acessar a pagina "https://code.visualstudio.com/sha/download?build=stable&os=linux-deb-x64"


