# ItauValidator

# Sobre a Soluçao

<p>Essa aplicação está escrita em C# (asp.net core) </p>
 
A solução contém dois projetos:
   * ItauValidator que expõem uma API que valida uma senha 
   * UnitTest contendo todos os unit tests da solução 

 # Sobre a API
 
  Como o Restful não tem um método específico para validações foi adotado o método GET:
  
  (GET) http://localhost:5000/api/v1/validator/pwd/AbTp9+fok
  
  ## Response:
  
  ### HTTP Codes Adotados:
  
  * 200 - Senha Válida
  * 400 - Senha Inválida
  * 500 - Erro Interno
  
  ### Body:
  
  * { "isValid": true }  - Senha Válida
  * { "isValid": false } - Senha Inválida ou Erro Interno
  
  **OBS**: Não foi considerado questões de segurança, como encriptação e decryptacao de senha para esta solução

#  Como executar a Aplicação

1. Ir na pasta do projeto ItauValidator:  **C:\<pasta_solucao>\ItauValidator\** 
2. Rodar o comando: **dotnet run**

# Como executar os unitTests

1. Ir na pasta do projeto UnitTest:  **C:\<pasta_solucao>\UnitTest\** 
2. Rodar o comando: **dotnet test**

# Postman Collection:

1. Localizado na Pasta:  **C:\<pasta_solucao>\Postman** 
