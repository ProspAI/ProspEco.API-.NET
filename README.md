# Proj_ProspEco API

**Proj_ProspEco** é uma API robusta e eficiente desenvolvida para promover o gerenciamento inteligente de recursos energéticos. O projeto envolve o controle de aparelhos, consumo de energia, bandeiras tarifárias, metas de consumo e conquistas, proporcionando um sistema completo para monitoramento e economia energética.

Esta API foi construída com boas práticas de desenvolvimento de software, incluindo injeção de dependência, design patterns e arquitetura em camadas para garantir escalabilidade, manutenção e performance.

---

## 📋 Funcionalidades

- **Gerenciamento de Aparelhos**: Suporte a operações CRUD (Create, Read, Update, Delete) para dispositivos de consumo energético.
- **Bandeiras Tarifárias**: Registro e consulta das bandeiras tarifárias ativas no período.
- **Metas de Consumo Energético**: Definição e controle de metas de consumo por usuários.
- **Notificações de Conquistas**: Sistema de notificações de conquistas quando metas de consumo são atingidas.
- **Consumo de Energia**: Registro do consumo de energia por usuários e aparelhos.
- **Documentação Swagger**: Documentação interativa da API utilizando Swagger.

---

## 🚀 Tecnologias Utilizadas

- **.NET Core 8.0**: Framework para o desenvolvimento da API.
- **Entity Framework Core**: ORM (Object-Relational Mapping) para integração com o banco de dados.
- **Oracle Database**: Banco de dados relacional para armazenar informações.
- **Swagger**: Para gerar a documentação interativa da API.
- **Dependency Injection**: Implementação de injeção de dependência para facilitar o gerenciamento e testabilidade de serviços e repositórios.
- **SOLID**: Aplicação dos princípios SOLID para promover um código limpo e fácil de manter.

---

## 🛠️ Setup do Projeto

### **Pré-requisitos**

- **.NET SDK 8.0** ou superior.
- **Oracle Database** configurado e em execução.
- Editor de código, como **Visual Studio Code** ou **Visual Studio**.
- Acesso e configuração da connection string no arquivo `appsettings.json`.

### **Passos para Instalação**

1. **Clone o repositório**:
    ```bash
    git clone <url-do-repositorio>
    ```

2. **Navegue para o diretório do projeto**:
    ```bash
    cd Proj_ProspEco
    ```

3. **Configure a string de conexão** no arquivo `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "OracleFIAP": "Data Source=<endereco_do_banco>;User Id=<usuario>;Password=<senha>;"
    }
    ```

4. **Restaure as dependências**:
    ```bash
    dotnet restore
    ```

5. **Execute o projeto**:
    ```bash
    dotnet run
    ```

6. **Acesse a documentação da API** via Swagger:
    - Abra o navegador e acesse o link: [http://localhost:<porta>/swagger](http://localhost:<porta>/swagger)

---

## 🧪 Endpoints da API

### **Aparelhos**

- **GET /api/aparelho**: Lista todos os aparelhos.
- **GET /api/aparelho/{id}**: Retorna um aparelho específico pelo ID.
- **POST /api/aparelho**: Cria um novo aparelho.
- **PUT /api/aparelho/{id}**: Atualiza um aparelho existente.
- **DELETE /api/aparelho/{id}**: Remove um aparelho pelo ID.

### **Bandeiras Tarifárias**

- **GET /api/bandeiras**: Lista todas as bandeiras tarifárias.
- **GET /api/bandeiras/{id}**: Retorna uma bandeira tarifária específica pelo ID.
- **POST /api/bandeiras**: Cria uma nova bandeira tarifária.
- **PUT /api/bandeiras/{id}**: Atualiza uma bandeira tarifária existente.
- **DELETE /api/bandeiras/{id}**: Remove uma bandeira tarifária.

### **Metas de Consumo**

- **GET /api/metas**: Lista todas as metas de consumo.
- **GET /api/metas/{id}**: Retorna uma meta de consumo específica pelo ID.
- **POST /api/metas**: Cria uma nova meta de consumo.
- **PUT /api/metas/{id}**: Atualiza uma meta de consumo existente.
- **DELETE /api/metas/{id}**: Remove uma meta de consumo.

---

## 🏗️ Estrutura do Projeto

A estrutura do projeto segue uma arquitetura em camadas para promover separação de responsabilidades, o que facilita a manutenção e evolução do código.
Proj_ProspEco/ ├── Controllers # Controladores da API (Endpoints) ├── Data # Configuração do DbContext e migrações ├── Models # Modelos de dados (entidades e DTOs) ├── Persistencia # Acesso a dados e lógica de negócios │ ├── Repositories # Repositórios para acesso ao banco de dados │ ├── Services # Lógica de negócios e serviços ├── Properties # Arquivos de configuração do projeto ├── appsettings.json # Configuração do ambiente e conexão com o banco └── Program.cs # Configuração do pipeline de execução da API

---

## ✅ Boas Práticas Aplicadas

- **SOLID**: Princípios de design aplicados em repositórios e serviços para promover um código mais modular e fácil de estender.
- **Injeção de Dependência**: Gerenciamento eficiente das dependências entre componentes.
- **Swagger**: Documentação da API disponível de forma interativa e acessível.
- **Clean Code**: Código limpo e bem estruturado para garantir fácil manutenção e evolução do projeto.

---

## 📚 Documentação

A documentação interativa da API está disponível via **Swagger**. Após executar a aplicação, você pode acessá-la em:

[http://localhost:<porta>/swagger](http://localhost:<porta>/swagger)

---

## 📌 Contribuindo

Contribuições são bem-vindas! Para contribuir com o projeto, siga as etapas abaixo:

1. **Fork** este repositório.
2. Crie uma nova branch:
    ```bash
    git checkout -b minha-feature
    ```
3. Faça suas alterações e **commit** as mudanças:
    ```bash
    git commit -m 'Minha nova feature'
    ```
4. **Push** para sua branch:
    ```bash
    git push origin minha-feature
    ```
5. Abra um **Pull Request** no repositório principal.

---

## Autores

- **AGATHA PIRES** – RM552247 – (2TDSPH)  
- **DAVID BRYAN VIANA** – RM551236 – (2TDSPM)  
- **GABRIEL LIMA** – RM99743 – (2TDSPM)  
- **GIOVANNA ALVAREZ** – RM98892 – (2TDSPM)  
- **MURILO MATOS** – RM552525 – (2TDSPM)  

---

## 📜 Licença

Este projeto está licenciado sob a **MIT License**. Consulte o arquivo `LICENSE` para mais detalhes.

