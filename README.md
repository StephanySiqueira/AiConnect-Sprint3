# SPRINT 3 - API de CRM AIConnect

## 1. Arquitetura da API

### Escolha da Arquitetura: Monolítica

#### Descrição:
A arquitetura monolítica foi escolhida para este projeto, onde todos os componentes da aplicação, como a gestão de **Clientes**, **Leads** e **Interações**, estão integrados em um único projeto. Toda a lógica de negócios, parâmetros e acesso a dados fazem parte da mesma aplicação, utilizando um único banco de dados.

#### Justificativa da Escolha:

##### 1. Simplicidade:
- **Desenvolvimento**: Uma estrutura monolítica simplifica o desenvolvimento, pois todos os componentes e recursos estão em um único código base. Isso facilita a navegação, o entendimento do código e a colaboração da equipe.
- **Implantação**: A implantação é direta, já que a aplicação inteira é empacotada e implantada como uma única unidade, eliminando a necessidade de coordenar múltiplos serviços.

##### 2. Menor Overhead Operacional:
- **Manutenção**: A manutenção é simplificada, pois todas as alterações são feitas em um único projeto, sem a necessidade de sincronizar mudanças entre diferentes serviços.
- **Gerenciamento de Recursos**: A infraestrutura necessária para suportar a aplicação é menos complexa, o que reduz o custo e o tempo de gerenciamento.

##### 3. Adequado para Aplicações de Pequeno a Médio Porte:
- Para aplicações que não exigem uma escalabilidade extremamente granular, uma arquitetura monolítica oferece todos os benefícios necessários sem a complexidade adicional dos microsserviços.

#### Diferenças na Implementação:

- **Arquitetura Monolítica**:
  - **Controle Centralizado**: Todos os recursos e controladores estão em um único projeto, o que facilita a integração e o gerenciamento.
  - **Banco de Dados Único**: A aplicação utiliza um único banco de dados para armazenar todas as informações, o que simplifica o acesso e a manutenção dos dados.

- **Arquitetura Microservices (não utilizada neste projeto)**:
  - **Controle Distribuído**: Cada microsserviço gerencia sua própria lógica de negócios e banco de dados, permitindo maior especialização e independência entre serviços.
  - **Banco de Dados Descentralizado**: Cada microsserviço pode ter seu próprio banco de dados, o que aumenta a complexidade, mas oferece maior flexibilidade e escalabilidade.

---

## 2. Padrões de Design Utilizados

- **Singleton**: Utilizado no gerenciamento de configurações através da classe `AppConfigurationManager`, garantindo que apenas uma instância do gerenciador de configurações seja criada e reutilizada em toda a aplicação.
  
- **DTO (Data Transfer Object)**: Utilizado para a transferência de dados entre a API e o cliente, garantindo que apenas as informações necessárias sejam expostas e manipuladas.

- **Repositório**: Utilizado para a interação com o banco de dados, separando a lógica de acesso a dados da lógica de negócios, garantindo maior organização e manutenibilidade.

---

## 3. Instruções para Rodar a API

### Pré-requisitos:
- .NET SDK 6.0 ou superior
- Banco de dados Oracle (configurado no arquivo `appsettings.json` com a chave `OracleConnection`)
- Visual Studio ou outro ambiente de desenvolvimento com suporte para .NET Core

### Passo a Passo:

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuprojeto/aiconnect-crm-api.git
   
2. Abra o projeto no Visual Studio ou no seu editor de código preferido.

3. Configure a string de conexão com o banco de dados Oracle no arquivo `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "OracleConnection": "Data Source=seubancodedados;User Id=seuusuario;Password=suasenha;"
     }
   }
4. Restaure as dependências do projeto:
   ```bash
   dotnet restore
   
5. Rode as migrações para criar o banco de dados:
    ```bash
    dotnet ef database update

6. Execute a API:
   ```bash
    dotnet run

7. Acesse a documentação do Swagger para explorar os endpoints disponíveis:
   - URL padrão: http://localhost:5000/swagger/index.html

## 4. Relacionamentos de Entidades

### Relação entre Cliente, Lead e Interação:
- **Lead**: Cada lead deve estar associado a um **Cliente** existente. O campo `ClienteId` no lead indica essa relação.
  
- **Interação**: Cada interação deve estar associada a um **Cliente** e a um **Lead**. O campo `ClienteId` na interação referencia o cliente, e o campo `LeadId` referencia o lead.

### Regras de Relacionamento:
1. **Lead sem Cliente**: Não é possível criar um lead sem um cliente válido. Se o `ClienteId` não existir, a API retornará o erro: **"ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA"**.
   
2. **Interação sem Cliente e Lead**: Não é possível criar uma interação se tanto o `ClienteId` quanto o `LeadId` forem inválidos ou inexistentes. Nesse caso, a API também retornará o erro: **"ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA"**.
   
3. **Interação com Cliente Válido e Lead Inválido**: Se o cliente existir, mas o lead não for encontrado, a interação falhará e o erro **"ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA"** será retornado.
   
4. **Interação com Cliente Inválido e Lead Válido**: Se o lead existir, mas o cliente não for encontrado, a interação falhará com a mesma mensagem de erro.

---

## 5. Membros do Grupo

- **Stephany Siqueira** RM: 98258
- **Camila Dos Santos Cunha** RM: 551785
- **Guilherme Castro** RM: 99624
- **Thiemi Hiratani Favaro** RM: 551478
- **Ana Clara Rocha de Oliveira** RM: 550110
