# MigradorArquivosFtp

Um dia eu precisei mover uma grande quantidade de arquivos de um FTP para outro, pegando os nomes das imagens de cada usuário, de um banco de dados MySql e criei o Migrador FTP. Depois de um tempo tive que excluir alguns arquivos, também buscando por usuário, então decidi criar um projeto com boas práticas como, DI e camadas, por fim precisei fazer o verificador.

O funcionamento é o seguinte:
- Adicionar as informações corretas no appSettings, uma connection string e dados dos FTPs (se não for usar o Migrador, basta adicionar o FTP de Origem).
- Adicionar um script SQL, eu já deixei um de modelo
- Executar o programa (Ele foi escrito para rodar em .NET 5.0)
- Escolher uma das opções 
- Digitar o código do usuário

Obs: existe uma configuração da busca de pasta que ele faz, talvez não atenda à todos os casos. Se a legenda for user path, busca de `upload/imoveis/{codigo do usuario}/nome da imagem`, se for rootpath busca de `upload/imoveis/nome da imagem`
