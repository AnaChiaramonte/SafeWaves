SafeWaves MVC

Sistema de Monitoramento IoT baseado em ASP.NET Core MVC

Visão Geral:

O SafeWaves MVC é um sistema web desenvolvido em ASP.NET Core MVC, integrado a dispositivos IoT via MQTT para monitoramento de ambientes residenciais.
O sistema recebe dados do ESP32, registra alertas e exibe informações em um dashboard restrito a usuários autenticados.
Também permite o envio de comandos MQTT, como abrir e fechar a porta remotamente.

Funcionalidades Principais:

-Autenticação e Controle de Acesso

Sistema de login e cadastro configurado com ASP.NET Core Identity.
Acesso ao dashboard permitido apenas para usuários autenticados.

-Integração com MQTT

Recebimento de mensagens enviadas pelo ESP32 por meio do endpoint:
POST /api/alertas/novo
Interpretação de sensores: movimento, gás, temperatura, umidade e porta.
Envio de comandos MQTT para abertura e fechamento da porta.

-Dashboard de Monitoramento

Visualização em tempo real dos alertas recebidos.
Interface simples e objetiva para acompanhamento dos dados.

-Histórico de Alertas

Registro completo dos alertas recebidos.
Listagem e consulta por tipo, data e conteúdo.

-Integração com Aplicativo Mobile

O sistema envia alertas para o aplicativo React Native por meio da mesma estrutura MQTT.

-Controle da Porta

Interface com botões para abrir e fechar a porta.
Acionamento via publicação MQTT.

Stack de Desenvolvimento:
Categoria	Tecnologia	Finalidade
Backend	ASP.NET Core MVC	Regras de negócio, controladores e APIs
Banco de Dados	SQL Server + EF Core	Persistência de dados
IoT / MQTT	MQTTnet	Comunicação com o ESP32 via MQTT
Mobile	React Native + MQTT	Recebimento de alertas no aplicativo
Autenticação	ASP.NET Core Identity	Login, proteção de rotas e controle de acesso
Interface Web	Razor + Bootstrap	Dashboard, páginas MVC e estilização

Instalação e Execução:
-Pré-requisitos:

.NET 9 SDK

SQL Server

Visual Studio 2022 ou VS Code

1. Clonar o repositório
git clone https://github.com/AnaChiaramonte/SafeWaves.git

2. Configuração do Banco de Dados

No arquivo appsettings.json, configure sua connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=SafeWaves;Trusted_Connection=True;"
}



3. Configuração do MQTT

Também no appsettings.json:

"MQTT": {
  "Server": "broker.hivemq.com",
  "TopicEntrada": "SafeWaves/alertas",
  "TopicSaida": "SafeWaves/comandos"
}


O ESP32 deve enviar os dados para:

POST /api/alertas/novo

4. Executar o Projeto
dotnet run

Endpoints:
Recebimento de Alertas do ESP32
POST /api/alertas/novo


Exemplo de payload:

{
  "tipo": "movimento",
  "mensagem": "Movimento detectado",
  "valor": "1"
}

Controle da Porta
POST /portas/abrir
POST /portas/fechar

Desenvolvimento

Projeto desenvolvido por Ana Clara Chiaramonte Lopes
