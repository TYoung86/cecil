//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2015 Jb Evain
// Copyright (c) 2008 - 2011 Novell, Inc.
//
// Licensed under the MIT/X11 license.
//

using System;
using System.Linq;

namespace Mono.Cecil {

	public class ParameterReference : IMetadataTokenProvider {

		string name;
		internal int index = -1;
		protected TypeReference parameter_type;
		internal IMethodSignature method;
		internal MetadataToken? token;

		public string Name {
			get { return name; }
			set { name = value; }
		}

		public int Index {
			get { return index; }
		}

		public TypeReference ParameterType {
			get { return parameter_type; }
			set { parameter_type = value; }
		}

		public IMethodSignature Method {
			get { return method; }
		}

		public virtual MetadataToken MetadataToken {
			get { return token ?? Resolve().MetadataToken; }
			set { token = value; }
		}

		internal ParameterReference (string name, TypeReference parameterType)
		{
			if (parameterType == null)
				throw new ArgumentNullException ("parameterType");

			this.name = name ?? string.Empty;
			this.parameter_type = parameterType;
		}
		
		internal ParameterReference (string name, int index, TypeReference parameterType)
		{
			if (parameterType == null)
				throw new ArgumentNullException ("parameterType");
			
			this.name = name ?? string.Empty;
			this.index = index >= -1 ? index : -1;
			this.parameter_type = parameterType;
		}
		
		internal ParameterReference (int index, TypeReference parameterType)
		{
			if (parameterType == null)
				throw new ArgumentNullException ("parameterType");

			this.index = index >= -1 ? index : -1;
			this.parameter_type = parameterType;
		}

		public ParameterReference (string name, int index, TypeReference parameterType, IMethodSignature method)
			: this(name, index, parameterType)
		{
			this.method = method;
		}

		public ParameterReference (int index, TypeReference parameterType, IMethodSignature method)
			: this(index, parameterType)
		{
			this.method = method;
		}
		
		public ParameterReference (string name, TypeReference parameterType, IMethodSignature method)
			: this(name, parameterType)
		{
			this.method = method;
		}

		public override string ToString ()
		{
			return name;
		}

		public virtual ParameterDefinition Resolve ()
		{
			if (index >= 0 && index < method.Parameters.Count) {
				var p = method.Parameters[index];
				if (p.Name == name)
					return p;
			}
			return ResolveByName();
		}

		internal ParameterDefinition ResolveByName ()
		{
			foreach (var p in method.Parameters) {
				if (p.Name == name)
					return p;
			}
			return null;
		}
	}
}
