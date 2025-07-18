# Documentação da API de Produtos e Categorias
## Esta documentação descreve os endpoints da API para gerenciamento de Produtos e Categorias, com exemplos de requisições utilizando cURL.
##
## Base URL: http://localhost:5000/api

## 1. Categoria Controller (/api/Categoria)
Gerencia as operações relacionadas a categorias.

# 1.1. Obter Todas as Categorias
Recupera uma lista de todas as categorias cadastradas.

Endpoint: GET /api/Categoria

Descrição: Retorna um array de objetos Categoria.

Status Codes:

200 OK: Sucesso.

Exemplo cURL:
Bash

curl -X GET "http://localhost:5000/api/Categoria"
# 1.2. Obter Categoria por ID
Recupera uma categoria específica pelo seu ID.

Endpoint: GET /api/Categoria/{id}

Descrição: Retorna um objeto Categoria correspondente ao ID fornecido.

Parâmetros de Rota:

id (GUID): O ID único da categoria.

Status Codes:

200 OK: Sucesso.

404 Not Found: Categoria não encontrada.

Exemplo cURL:
Bash

curl -X GET "http://localhost:5000/api/Categoria/9b0d2a4f-7b1e-4c8d-8c0e-6f7a8b9c0d1e"
Nota: Substitua 9b0d2a4f-7b1e-4c8d-8c0e-6f7a8b9c0d1e pelo ID real da categoria.

# 1.3. Criar Nova Categoria
Cria uma nova categoria.

Endpoint: POST /api/Categoria

Descrição: Adiciona uma nova categoria ao sistema.

Request Body (JSON):

nome (string, obrigatório): O nome da categoria.

Status Codes:

201 Created: Categoria criada com sucesso. Retorna a categoria criada e o cabeçalho Location com a URL do novo recurso.

400 Bad Request: Requisição inválida (por exemplo, nome ausente).

Exemplo cURL:
Bash

curl -X POST "http://localhost:5000/api/Categoria" \
     -H "Content-Type: application/json" \
     -d '{
           "nome": "Eletrônicos"
         }'
## 2. Produto Controller (/api/Produto)
Gerencia as operações relacionadas a produtos.

# 2.1. Obter Todos os Produtos
Recupera uma lista de todos os produtos cadastrados.

Endpoint: GET /api/Produto

Descrição: Retorna um array de objetos ProdutoDto.

Status Codes:

200 OK: Sucesso.

Exemplo cURL:
Bash

curl -X GET "http://localhost:5000/api/Produto"
# 2.2. Obter Produto por ID
Recupera um produto específico pelo seu ID.

Endpoint: GET /api/Produto/{id}

Descrição: Retorna um objeto ProdutoDto correspondente ao ID fornecido.

Parâmetros de Rota:

id (GUID): O ID único do produto.

Status Codes:

200 OK: Sucesso.

404 Not Found: Produto não encontrado.

Exemplo cURL:
Bash

curl -X GET "http://localhost:5000/api/Produto/a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"
Nota: Substitua a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d pelo ID real do produto.

# 2.3. Adicionar Novo Produto
Adiciona um novo produto.

Endpoint: POST /api/Produto

Descrição: Adiciona um novo produto ao sistema.

Request Body (JSON):

id (GUID): Opcional, será gerado se não for fornecido.

nome (string, obrigatório): O nome do produto.

descricao (string, obrigatório): Uma descrição do produto.

preco (decimal, obrigatório): O preço do produto.

categoriaId (GUID, obrigatório): O ID da categoria à qual o produto pertence.

Status Codes:

201 Created: Produto criado com sucesso. Retorna o DTO do produto criado e o cabeçalho Location com a URL do novo recurso.

400 Bad Request: Requisição inválida (por exemplo, campos ausentes ou inválidos).

Exemplo cURL:
Bash

curl -X POST "http://localhost:5000/api/Produto" \
     -H "Content-Type: application/json" \
     -d '{
           "nome": "Smartphone Modelo X",
           "descricao": "Um smartphone de última geração com câmera de alta resolução.",
           "preco": 1200.50,
           "categoriaId": "9b0d2a4f-7b1e-4c8d-8c0e-6f7a8b9c0d1e"
         }'
Nota: Substitua 9b0d2a4f-7b1e-4c8d-8c0e-6f7a8b9c0d1e pelo ID real de uma categoria existente.

# 2.4. Atualizar Produto Existente
Atualiza um produto existente.

Endpoint: PUT /api/Produto/{id}

Descrição: Atualiza as informações de um produto existente com base no seu ID.

Parâmetros de Rota:

id (GUID): O ID único do produto a ser atualizado.

Request Body (JSON):

id (GUID, obrigatório): O ID do produto. Deve ser o mesmo que o ID na URL.

nome (string, obrigatório): O nome atualizado do produto.

descricao (string, obrigatório): A descrição atualizada do produto.

preco (decimal, obrigatório): O preço atualizado do produto.

categoriaId (GUID, obrigatório): O ID da categoria atualizada.

Status Codes:

204 No Content: Produto atualizado com sucesso (nenhum conteúdo é retornado no corpo da resposta).

400 Bad Request: Requisição inválida (por exemplo, ID na URL difere do ID no corpo).

404 Not Found: Produto não encontrado (se o ID não corresponder a nenhum produto existente no _serviceProduto.AtualizarProduto).

Exemplo cURL:
Bash

curl -X PUT "http://localhost:5000/api/Produto/a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d" \
     -H "Content-Type: application/json" \
     -d '{
           "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
           "nome": "Smartphone Modelo X (Atualizado)",
           "descricao": "Descrição atualizada para o smartphone X.",
           "preco": 1150.00,
           "categoriaId": "9b0d2a4f-7b1e-4c8d-8c0e-6f7a8b9c0d1e"
         }'
Nota: Substitua os IDs pelos valores reais.

# 2.5. Remover Produto
Remove um produto existente.

Endpoint: DELETE /api/Produto/{id}

Descrição: Remove um produto do sistema com base no seu ID.

Parâmetros de Rota:

id (GUID): O ID único do produto a ser removido.

Status Codes:

204 No Content: Produto removido com sucesso.

404 Not Found: Produto não encontrado (se o ID não corresponder a nenhum produto existente no _serviceProduto.RemoverProduto).

Exemplo cURL:
Bash

curl -X DELETE "http://localhost:5000/api/Produto/a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"
Nota: Substitua a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d pelo ID real do produto.
