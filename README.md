### 🌊 **SafeWaves MVC**
Sistema de Monitoramento IoT com ASP.NET Core MVC

O SafeWaves MVC é um sistema web para monitoramento residencial em tempo real, integrando ASP.NET Core MVC, MQTT e dispositivos IoT como o ESP32.
Ele coleta alertas de sensores, exibe dados em um dashboard protegido e permite o envio de comandos remotos — como abrir ou fechar portas.


### **Funcionalidades**


🔐 **Autenticação e Controle de Acesso**

-Implementado com ASP.NET Core Identity.
-Apenas usuários autenticados podem acessar o dashboard e o controle da porta.


📡 **Integração com MQTT**

-Recebe dados do ESP32 via MQTTnet.
-Suporte a sensores de movimento, gás, temperatura, umidade e porta.
-Envio de comandos MQTT para abertura/fechamento da porta.


📊 **Dashboard em Tempo Real**

-Interface intuitiva usando Razor + Bootstrap.
-Atualização instantânea dos alertas recebidos.
-Visualização rápida do estado dos sensores.


🗂️ **Histórico de Alertas**

-Registro completo no banco de dados.
-Filtros por tipo, data e conteúdo do alerta.


📱 **Integração com App Mobile**

-Envio dos mesmos alertas para o aplicativo React Native, também via MQTT.


🚪 **Controle da Porta**

-Botões para abrir e fechar a porta remotamente.
-Ações enviadas via publicação MQTT.


### **Stack de Desenvolvimento**

| Categoria        | Tecnologia            | Finalidade                                 |
|------------------|------------------------|---------------------------------------------|
| Backend          | ASP.NET Core MVC       | Regras de negócio, APIs e controllers       |
| Banco de Dados   | SQL Server + EF Core   | Persistência de dados                       |
| IoT / MQTT       | MQTTnet                | Comunicação com ESP32 via MQTT              |
| Mobile           | React Native + MQTT    | Recebimento de alertas no aplicativo        |
| Autenticação     | ASP.NET Core Identity  | Login, roles e controle de acesso           |
| Interface Web    | Razor + Bootstrap      | Dashboard e páginas web                     |

### **Instalação e Execução**
📌 **Pré-requisitos**

.NET 9 SDK

SQL Server

Visual Studio 2022 ou VS Code

**1️. Clonar o repositório**

git clone https://github.com/AnaChiaramonte/SafeWaves.git

**2️. Configurar o Banco de Dados**

No arquivo appsettings.json, ajuste a conexão:

"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=SafeWaves;Trusted_Connection=True;"
}

**3️. Configurar MQTT**

Ainda no appsettings.json:

"MQTT": {
  "Server": "broker.hivemq.com",
  "TopicEntrada": "SafeWaves/alertas",
  "TopicSaida": "SafeWaves/comandos"
}


O ESP32 envia alertas via POST para:

/api/alertas/novo

**4️. Executar o Projeto**

-dotnet run

Endpoints Principais:

-POST /api/alertas/novo

Exemplo de payload:

{
  "tipo": "movimento",
  "mensagem": "Movimento detectado",
  "valor": "1"
}

### 👩‍💻** Desenvolvimento**

Projeto desenvolvido por Ana Clara Chiaramonte Lopes
