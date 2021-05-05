% message passing demo.
-module(main).
-export([start/0, mainMethod/2, downloadAvatar/1, checkLogin/1]).

checkLogin(Main) ->
  io:format("Checking login information\n"),
  timer:sleep(3000),
  Result = true, % pretend this is a real check.
  Main ! {self(), loginResult, Result}.

downloadAvatar(Main) ->
  io:format("Downloading avatar...\n"),
  timer:sleep(5000),
  Result = "Avatar URL", % pretend this is a real work.
  Main ! {self(), avatarResult, Result}.

writeLog(Main, LoginResult, AvatarResult) -> 
  io:format("Writing log: login ~s, avatar ~s\n", LoginResult, AvatarResult),
  timer:sleep(5000),
  Main ! {self(), writeLogResult}.

mainMethod(LoginResult, AvatarResult) ->
  io:format("MainMethod loop\n"),
  if 
  (LoginResult == nil) or (AvatarResult == nil) ->
    receive
      {_, loginResult, Result} -> mainMethod(Result, AvatarResult);
      {_, avatarResult, Result} -> mainMethod(LoginResult, Result)
    end;
  true -> 
    spawn(demo, writeLog, []),
    receive
      _ -> io:format("Done!\n")
    end
  end.

start() ->
  R = spawn(demo, mainMethod, [nil, nil]),
  spawn(demo, checkLogin, [R]),
  spawn(demo, downloadAvatar, [R]).
  