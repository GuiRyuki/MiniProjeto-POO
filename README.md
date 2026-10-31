# MiniProjeto-POO
Oficina Prática PBL — Plataforma de Apoio ao Estudo (**FecapStudy**).

## Estrutura

```
Backend/                 BackendEstudoAPI (C# + MySQL, porta 5080), fornecido pela aula
ProjetoPOO/
├── FecapStudy.slnx      Solução
├── ProjetoPOO/          Biblioteca com o domínio (Missões 1 a 8) + ClienteBackendEstudo (Missão 10)
├── FecapStudyConsole/   Demonstração em texto das Missões 1 a 8
└── FecapStudyUI/        Tela Windows Forms (Missões 9 e 10)
```

## Como executar

1. Inicie o backend: `Backend\iniciar_backend.bat` (painel em http://localhost:5080).
   Sem MySQL, ele usa automaticamente o arquivo `dados_estudo_local.json`.
2. Abra a tela, de dentro da pasta `ProjetoPOO`:
   ```
   dotnet run --project FecapStudyUI
   ```
   No Visual Studio: abra `FecapStudy.slnx` e defina **FecapStudyUI** como projeto de inicialização.
3. (Opcional) Demonstração no terminal: `dotnet run --project FecapStudyConsole`

## Conceitos de POO aplicados

| Conceito | Onde |
|---|---|
| Abstração, classes e objetos | `Disciplina` |
| Encapsulamento | `MetaEstudo`, `Usuario` (senha sem getter) |
| Classe abstrata e herança | `Usuario` → `Administrador`, `Responsavel`, `Discente` |
| Polimorfismo dinâmico | `SessaoEstudo` → `SessaoPomodoro`, `SessaoExercicios`, `SessaoLeitura` |
| Polimorfismo estático | `Discente.RegistrarEstudo(...)` com 3 sobrecargas |
| Interfaces | `INotificador` (E-mail, WhatsApp, Painel) e `IExportavelJson` |
| Associação e agregação | `Responsavel` ——> `Discente`, `TrilhaEstudo` ◇── `Disciplina` |
| Composição e dependência | `PlanoEstudo` ◆── `ItemCronograma`, `ServicoAlertaResponsavel` - - -> `INotificador` |
| GUI + backend | `FormPrincipal` + `ClienteBackendEstudo` (POST/GET em `http://localhost:5080`) |
