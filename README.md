# PedidoProdutoCliente

Este é um projeto feito em C# de uma Web API usando .NET 8 para um CRUD básico que trabalha as relações entre Pedido, Cliente e Produto.

## 🧱 Estrutura do Projeto

O projeto é dividido nas seguintes camadas:

- **API**
- **Application**
- **Domain**
- **Infrastructure**
- **Test**

## ✅ Testes

- Utiliza **xUnit** para testes unitários.

## 🔄 Regras de Negócio

- Ao adicionar produtos ao pedido:
  - O valor total do pedido é automaticamente calculado com base nos preços dos produtos.
  - A quantidade em estoque do produto é subtraída.
  - É validado se a quantidade do produto é maior que zero antes da adição.

## 📑 Swagger

- O projeto possui o Swagger para facilitar as requisições e a documentação da API.

## 🐘 Banco de Dados com Docker

Utilizei uma imagem Docker do PostgreSQL para testar o desenvolvimento.

```bash
docker run -d --name postgres_pedidoprodutocliente --hostname postgres_host -e POSTGRES_PASSWORD=admin@123 -p 5432:5432 postgres
```

> A aplicação está configurada para se conectar ao banco com a senha `admin@123`.

## ⚙️ Aplicação de Migrations Automáticas

Foi usada uma abordagem de DataSeeder, ao iniciar a aplicação, todas as migrations existentes são aplicadas automaticamente ao banco de dados, sem a necessidade de executar comandos como `update-database`.

## 🧠 Modelo de Domínio

### Relações principais

- **Cliente (1:N) Pedido**
- **Pedido (N:N) Produto**

### 📄 Cliente

| Campo                   | Tipo     |
|-------------------------|----------|
| id                      | int      |
| nome                    | string   |
| cpf                     | string   |
| email                   | string   |
| telefone                | string   |
| endereco                | string   |
| dataCadastro            | DateTime |
| dataUltimaAtualizacao   | DateTime |
| dataExclusao            | DateTime |
| pedidos                 | Lista (abstract) |

### 📄 Pedido

| Campo                   | Tipo     |
|-------------------------|----------|
| id                      | int      |
| clienteId               | int      |
| formaPagamento          | string   |
| parcelas                | int      |
| valorSubtotal           | decimal  |
| valorParcela            | decimal  |
| valorTotal              | decimal  |
| observacoes             | string   |
| dataPedido              | DateTime |
| dataUltimaAtualizacao   | DateTime |
| dataExclusao            | DateTime |
| cliente                 | Referência (abstract) |
| produtos                | Lista (abstract) |

### 📄 Produto

| Campo                   | Tipo     |
|-------------------------|----------|
| id                      | int      |
| nome                    | string   |
| descricao               | string   |
| valor                   | decimal  |
| quantidade              | int      |
| dataCadastro            | DateTime |
| dataUltimaAtualizacao   | DateTime |
| dataExclusao            | DateTime |
| pedidos                 | Lista (abstract) |

## 🛠️ Consultas SQL Úteis

```sql
SELECT * FROM public."Clientes" ORDER BY 1 DESC LIMIT 100;
SELECT * FROM public."Produtos" ORDER BY 1 DESC LIMIT 100;
SELECT * FROM public."Pedidos" ORDER BY 1 DESC LIMIT 100;
```

## 🔁 Exemplos de Requisições no Swagger

### ➕ Adicionar Cliente

```json
{
  "nome": "Paulo Teles",
  "cpf": "01010101010",
  "email": "teste@teste.com"
}
```

```json
{
  "nome": "Rita Santos",
  "cpf": "02020202020",
  "email": "xxx@xxx.com",
  "telefone": "85999998888",
  "endereco": "Rua da Rita, 610"
}
```

```json
{
  "nome": "Priscila Ramos",
  "cpf": "03030303030",
  "email": "teste@xxx.com",
  "telefone": "85999997777",
  "endereco": "Rua da Julia, 50"
}
```

### ❌ CPF, email e telefone inválidos

```json
{
  "nome": "Chico Oliveira",
  "cpf": "0202020202",
  "email": "xxx",
  "telefone": "859999988887777"
}
```

### ❌ CPF duplicado

```json
{
  "nome": "Chico Oliveira",
  "cpf": "03030303031",
  "email": "xxx@teste.com.br",
  "telefone": "85999996666"
}
```

### ✏️ Atualizar Cliente (endereço)

```json
{
  "id": 3,
  "endereco": "Rua da Julia, 51"
}
```

### ➕ Adicionar Produto

```json
{
  "nome": "Monitor",
  "descricao": "AOC",
  "valor": 400.00,
  "quantidade": 2
}
```

```json
{
  "nome": "Teclado",
  "descricao": "Multi",
  "valor": 35.50,
  "quantidade": 10
}
```

```json
{
  "nome": "Mouse",
  "descricao": "Logi",
  "valor": 50.00,
  "quantidade": 20
}
```

```json
{
  "nome": "Teste",
  "descricao": "teste",
  "valor": 1.00,
  "quantidade": 100
}
```

### ✏️ Atualizar Produto

```json
{
  "id": 1,
  "quantidade": 150
}
```

### ➕ Adicionar Pedido

```json
{
  "clienteId": 1,
  "pagamentoForma": "PIX",
  "parcelas": 2,
  "observacoes": "teste",
  "produtosId": [1, 2]
}
```
