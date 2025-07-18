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


