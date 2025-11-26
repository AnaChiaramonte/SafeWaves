🛡️ SafeWaves | Monitoramento Inteligente de Idosos

📝 Índice
1. Visão Geral do SafeWaves MVC
2. Funcionalidades
3. Stack de Desenvolvimento
4. Instalação e Execução

---

## 1. Visão Geral do SafeWaves MVC

O **SafeWaves** é um sistema web desenvolvido em ASP.NET Core MVC (com Blazor no frontend) para monitoramento inteligente e não invasivo de idosos em ambientes residenciais. O sistema integra sensores IoT via MQTT, armazena e exibe leituras, e emite alertas em tempo real para familiares e cuidadores utilizando SignalR.

O objetivo é promover segurança, autonomia e resposta rápida a situações de risco, como quedas ou ausência prolongada de movimento.

---

## 2. Funcionalidades

- **Cadastro e Gerenciamento de Usuários:** Idosos, cuidadores, responsáveis e administradores.
- **Cadastro de Sensores:** Associação de sensores a usuários e monitoramento de ambientes.
- **Leitura de Sensores:** Visualização de dados históricos e em tempo real.
- **Zonas Seguras:** Definição de áreas monitoradas e zonas de risco.
- **Alertas em Tempo Real:** Recebimento de alertas automáticos via MQTT e notificação instantânea para todos os usuários conectados via SignalR.
- **Contatos de Emergência:** Cadastro de contatos para acionamento rápido em situações críticas.
- **Interface Web Responsiva:** Navegação intuitiva e acessível.

---

## 3. Stack de Desenvolvimento

| Categoria         | Tecnologia                        | Uso                                 |
|-------------------|-----------------------------------|-------------------------------------|
| Backend           | ASP.NET Core MVC                  | Lógica de negócio e APIs            |
| Frontend          | Blazor Server                     | Interface web interativa            |
| Banco de Dados    | Entity Framework Core + SQL Server| Persistência de dados               |
| IoT/MQTT          | MQTTnet                           | Integração com sensores             |
| Tempo real        | SignalR                           | Notificações instantâneas           |
| UI                | Bootstrap                         | Estilização                         |
| Autenticação      | ASP.NET Identity                  | Controle de acesso                  |

---

## 4. Instalação e Execução

### 📦 Pré-requisitos
- .NET 9 SDK
- SQL Server (ou outro banco de dados relacional)

### 💻 Instalação
