# Cenário 1: Testar criação de coleta com sucesso

Feature: Criação de Coleta
  As a user
  I want to create a coleta
  So that I can manage it in the system

  Scenario: Sucesso ao criar coleta
    Given que estou autenticado no sistema
    When eu envio uma solicitação de criação de coleta com dados válidos
    Then a coleta deve ser criada com sucesso
    And devo receber um status 201

# Cenário 2: Falha ao criar coleta sem dados

  Scenario: Falha ao criar coleta sem dados
    Given que estou autenticado no sistema
    When eu envio uma solicitação de criação de coleta sem dados
    Then o sistema deve retornar um erro de validação
    And devo receber um status 400

# Cenário 3: Falha ao criar coleta com dados inválidos

  Scenario: Falha ao criar coleta com dados inválidos
    Given que estou autenticado no sistema
    When eu envio uma solicitação de criação de coleta com dados inválidos
    Then o sistema deve retornar um erro de validação
    And devo receber um status 422
