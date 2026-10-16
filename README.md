# Buscar Professores Disponíveis

Um teste que recebi para desenvolver uma API que busca professores disponíveis de acordo com as informações solicitadas pelo cliente.

Como o projeto foi desenvolvido para testar meus conhecimentos em C# e .NET, não utilizei ferramentas de IA. Todo o desenvolvimento foi feito consultando a documentação oficial da Microsoft e utilizando os conhecimentos que já possuo com a linguagem.

**Tecnologias utilizadas:** C# 8.0, .NET 8 e SQL Server.

## 1. API (C# e .NET 8)

### Buscar professores disponíveis

`POST /api/professores/busca`

**Requisição:**

```json
{
  "idioma": "Espanhol",
  "dias": ["segunda"],
  "inicioFaixa": "18:00",
  "fimFaixa": "20:00",
  "duracaoMinutos": 60
}
```

**Resposta:**

```json
{
  "professores": [
    {
      "id": 1,
      "nome": "Caio",
      "aulasNaSemana": 1,
      "horarios": ["18:00"]
    },
    {
      "id": 4,
      "nome": "Diego",
      "aulasNaSemana": 1,
      "horarios": ["18:00", "18:30", "19:00"]
    }
  ],
  "naoElegiveis": [
    {
      "id": 2,
      "nome": "Bruno",
      "motivo": "Não ensina o idioma."
    }
  ]
}
```

### Outras requisições

**Agendar aula**

`POST /api/agendamento/agendar`

```json
{
  "professorId": 2,
  "dia": "segunda",
  "inicio": "20:00:00",
  "duracaoMinutos": 60
}
```

**Criar disponibilidade**

`POST /api/disponibilidade/criar`

```json
{
  "professorId": 2,
  "dia": "segunda",
  "inicio": "20:00:00",
  "fim": "21:00:00"
}
```

**Cadastrar professor**

`POST /api/professores/cadastrar`

```json
{
  "nome": "Caio",
  "idiomas": [
    "Ingles",
    "Historia"
  ]
}
```

## 2. Regras de busca

1. O professor precisa ensinar o idioma solicitado.
2. Professores com a agenda bloqueada não aparecem nos resultados.
3. O aluno deseja um horário fixo: o mesmo horário de início precisa estar livre em todos os dias solicitados.
4. A aula precisa caber integralmente dentro de um bloco de disponibilidade do professor e dentro da faixa de horário solicitada pelo aluno.
5. A aula não pode se sobrepor a outra já agendada. Horários consecutivos são permitidos: uma aula que termina às 19:00 permite outra começando às 19:00.
6. As aulas só podem começar em horários cheios ou na meia hora (`:00` ou `:30`).
7. Os professores são ordenados primeiro pela quantidade de aulas agendadas na semana, da menor para a maior. Em caso de empate, é utilizada a ordem alfabética do nome. Os horários de cada professor também são apresentados em ordem crescente.

Os professores não elegíveis são retornados com o motivo correspondente, utilizando um dos seguintes textos:

- `Não ensina o idioma.`
- `Agenda bloqueada.`
- `Sem horário em comum nos dias pedidos.`

## 3. Validação

A API retorna o código HTTP `400 Bad Request`, acompanhado de uma mensagem clara, quando:

- O idioma está vazio.
- Nenhum dia foi informado.
- Algum dia informado é inválido.
- Algum horário está fora do formato `HH:mm`.
- O fim da faixa de horário é igual ou anterior ao início.
- A duração da aula é diferente de 30, 60 ou 90 minutos.

## 4. Testes automatizados

Foram implementados testes automatizados em C# para validar as regras de busca de professores disponíveis.

Os testes verificam o comportamento da lógica de negócio sem a necessidade de iniciar a API.
