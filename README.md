# SPRINT 3 - API de CRM AIConnect
## 1. Arquitetura da API
### Escolha da Arquitetura: Monolítica
#### Descrição:
A arquitetura monolítica foi escolhida para este projeto, onde todos os componentes da aplicação, como a gestão de Clientes, Leads e Interações, estão integrados em um único projeto. Toda a lógica de negócios, parâmetros e acesso a dados fazem parte da mesma aplicação, utilizando um único banco de dados.

#### Justificativa da Escolha:
#### Simplicidade:

Desenvolvimento : Uma estrutura monolítica simplifica o desenvolvimento, pois todos os componentes e recursos estão em um único código base. Isso facilita a navegação, o entendimento do código e a colaboração da equipe.
Implantação : A implantação é direta, já que a aplicação inteira é embalada e implantada como uma única unidade, eliminando a necessidade de coordenar múltiplos serviços.
Menor Overhead Operacional:

Manutenção : A manutenção é simplificada, pois todas as alterações são feitas em um único projeto, sem a necessidade de sincronizar mudanças entre diferentes serviços.
Gerenciamento de Recursos : A infraestrutura necessária para suportar a aplicação é menos complexa, mais rápida e mais rápida de gerenciamento.
Adequado para Aplicações de Pequeno a Médio Porte:

Para aplicações que não bloqueiam uma escalabilidade extremamente granular, uma arquitetura monolítica oferece todos os benefícios necessários sem a complexidade adicional dos microsserviços.
Diferenças na Implementação:
Arquitetura Monolítica:

Controle Centralizado : Todos os recursos e controladores estão em um único projeto, o que facilita a integração e o gerenciamento.
Banco de Dados Único : A aplicação utiliza um único banco de dados para armazenar todas as informações, o que simplifica o acesso e a manutenção dos dados.
Arquitetura Microservices (não utilizada neste projeto):

Controle Distribuído : Cada microsserviço gerencia sua própria lógica de negócios e banco de dados, permitindo maior especialização e independência entre serviços.
Banco de Dados Descentralizado : Cada microsserviço pode ter seu próprio banco de dados, o que aumenta a complexidade, mas oferece maior flexibilidade e escalabilidade.
2. Padrões de Design Utilizados
Singleton : Utilizado no gerenciamento de configurações através da classe AppConfigurationManager, garantindo que apenas uma instância do gerenciador de configurações seja criada e reutilizada em toda a aplicação.

DTO (Data Transfer Object) : Para a transferência de dados entre a API e o cliente, garantindo que apenas as informações necessárias sejam expostas e manipuladas.

Repositório : Utilizado para a interação com o banco de dados, separando a lógica de acesso a dados da lógica de negócios, garantindo maior organização e manutenibilidade.

3. Instruções para rodar a API
Pré-requisitos
.NET SDK 6.0 ou superior
Banco de dados Oracle (configurado no arquivo appsettings.jsoncom a chave OracleConnection)
Visual Studio ou outro ambiente de desenvolvimento com suporte para .NET Core
