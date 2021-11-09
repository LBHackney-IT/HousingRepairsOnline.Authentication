.PHONY: build
build:
	dotnet build

.PHONY: test
test:
	dotnet test

.PHONY: lint
lint:
	-dotnet tool install dotnet-format --tool-path ./local-tools/dotnet-format/
	./local-tools/dotnet-format/dotnet-format
