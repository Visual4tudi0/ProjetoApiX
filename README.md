### APi para vendas de produtos

## Rota Base para acessar 
https://localhost:44325/swagger/index.html

## No controller 

A API utilizará os verbos HTTP padrões: GET, GET por ID, POST, PUT e DELETE.

GET: Realiza uma busca geral, retornando todos os dados disponíveis.

GET por ID: Realiza uma busca específica de um registro, utilizando seu (ID).

POST: Utilizado para adicionar novos dados ao banco de dados.

Para Categoria: Basta informar o nome da categoria e o ID será adicionado automaticamente, sem a necessidade de sua adição manual.

Para Produto: É necessário informar o nome, um valor maior que zero e um estoque também maior que zero. Além disso, deve-se informar o ID da categoria à qual o produto pertence. É importante ressaltar que, para um produto ser cadastrado, a categoria correspondente já deve existir.

## Camada Service 
O service é onde esta toda a logica do funcionamento da APi

# 📦 API - Sistema de Produtos e Categorias

**Base URL:** `http://localhost:5000/api`

---

## 📘 Endpoints disponíveis

### ✅ ProdutoController

#### 🔍 Listar todos os produtos

```bash
curl -X GET http://localhost:5000/api/produto
🔎 Buscar produto por ID
bash
Copiar
Editar
curl -X GET http://localhost:5000/api/produto/{id}
Substitua {id} pelo GUID do produto.

➕ Adicionar produto
bash
Copiar
Editar
curl -X POST http://localhost:5000/api/produto \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Exemplo de Produto",
    "descricao": "Descrição do produto",
    "quantidadeEstoque": 100,
    "codigoDeBarras": "7891234567890",
    "marca": "Marca Exemplo",
    "id": "opcional"
}'
✏️ Atualizar produto
bash
Copiar
Editar
curl -X PUT http://localhost:5000/api/produto/{id} \
  -H "Content-Type: application/json" \
  -d '{
    "id": "{id}",
    "nome": "Produto Atualizado",
    "descricao": "Nova descrição",
    "quantidadeEstoque": 80,
    "codigoDeBarras": "7891234567890",
    "marca": "Marca Atualizada"
}'
🗑️ Remover produto
bash
Copiar
Editar
curl -X DELETE http://localhost:5000/api/produto/{id}
📂 CategoriaController
📋 Listar todas as categorias
bash
Copiar
Editar
curl -X GET http://localhost:5000/api/categoria
🔎 Buscar categoria por ID
bash
Copiar
Editar
curl -X GET http://localhost:5000/api/categoria/{id}
➕ Adicionar categoria
bash
Copiar
Editar
curl -X POST http://localhost:5000/api/categoria \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Categoria Exemplo",
    "descricao": "Descrição da categoria"
}'
